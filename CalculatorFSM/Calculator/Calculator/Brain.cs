using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    enum CalcState
    {
        Zero,
        AccumulateDigits,
        Operation,
        Result,
        MemorySave,
        SinCos,
        Modulo,
        Logarithm,
        ParticularPower,
        PowerOfTen,
        Factorial,
        AnyRoot,
        Expression
    }

    public delegate void ChangeTextDelegate(string text);

    class Brain
    {
        ChangeTextDelegate changeTextDelegate;
        ChangeTextDelegate changeRecurringTextDelegate;
        CalcState calcState = CalcState.Zero;
        string firstNumber = "";
        string secondNumber = "";
        string resultNumber = "";
        string operation = "";
        bool afterResult = false;
        bool resultToMemory = false;
        bool memoryNumberToFirst = false;
        List<string> reccuring = new List<string>();
        List<string> memoryBox = new List<string>();
        string memoryNumber = "0";
        Stack<int> sumNumbers = new Stack<int>();
        Stack<string> operations = new Stack<string>();
        Stack<string> signs = new Stack<string>();
        int precendence = 0;
        bool isExpression = false;
        bool isAdvancedExpression = false;

        public Brain(ChangeTextDelegate changeTextDelegate, ChangeTextDelegate changeRecurringTextDelegate)
        {
            this.changeTextDelegate = changeTextDelegate;
            this.changeRecurringTextDelegate = changeRecurringTextDelegate;
        }

        public void Process(string msg)
        {
            switch (calcState)
            {
                case CalcState.Zero:
                    Zero(msg, false);
                    break;
                case CalcState.AccumulateDigits:
                    AccumulateDigits(msg, false);
                    break;
                case CalcState.Operation:
                    Operation(msg, false);
                    break;
                case CalcState.Result:
                    Result(msg, false);
                    break;
                case CalcState.MemorySave:
                    MemorySave(msg, false);
                    break;
                case CalcState.SinCos:
                    SinCos(msg, false);
                    break;
                case CalcState.Modulo:
                    Modulo(msg, false);
                    break;
                case CalcState.Logarithm:
                    Logarithm(msg, false);
                    break;
                case CalcState.ParticularPower:
                    ParticularPower(msg, false);
                    break;
                case CalcState.PowerOfTen:
                    PowerOfTen(msg, false);
                    break;
                case CalcState.Factorial:
                    Factorial(msg, false);
                    break;
                case CalcState.AnyRoot:
                    AnyRoot(msg, false);
                    break;
                case CalcState.Expression:
                    Expression(msg, false);
                    break;
                default:
                    break;
            }
        }

        void Zero(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Zero;
            }

            else
            {
                if (Rules.IsNonZeroDigit(msg))
                    AccumulateDigits(msg, true);
                if (Rules.IsSeparator(msg))
                    AccumulateDigits(msg, true);
                if (Rules.IsMemoryOperation(msg))
                {
                    memoryNumberToFirst = true;
                    MemorySave(msg, true);
                }
            }
        }

        void AccumulateDigits(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.AccumulateDigits;

                if (msg == "," && secondNumber.Contains(',') == false)
                {
                    secondNumber = "0,";
                    changeTextDelegate.Invoke(secondNumber);
                }

                else
                {
                    secondNumber += msg;
                    changeTextDelegate.Invoke(secondNumber);
                }
            }

            else
            {
                if (Rules.IsDigit(msg))
                {
                    secondNumber += msg;
                    changeTextDelegate.Invoke(secondNumber);
                }

                else if (Rules.IsSeparator(msg))
                {
                    if (secondNumber.Contains(',') == false)
                    {
                        secondNumber += ",";
                        changeTextDelegate.Invoke(secondNumber);
                    }
                }

                else if (Rules.IsChangingSign(msg))
                {
                    if (secondNumber[0] == '-')
                        secondNumber = secondNumber.Remove(0, 1);
                    else
                        secondNumber = "-" + secondNumber;

                    changeTextDelegate.Invoke(secondNumber);
                }

                else if (Rules.IsOperation(msg))
                {
                    Operation(msg, true);
                    reccuring.Add(msg);
                    string text = string.Join(" ", reccuring.ToArray());
                    changeRecurringTextDelegate.Invoke(text);
                }

                else if (Rules.IsPercent(msg))
                {
                    if (operation == "+" || operation == "-")
                        CalculatePercent();
                    else if (operation == "x" || operation == "÷")
                        CalculateSecondPercent();
                    changeTextDelegate.Invoke(secondNumber);
                }

                else if (Rules.IsResult(msg))
                    Result(msg, true);

                else if (Rules.IsFullReset(msg))
                    FullReset();

                else if (Rules.IsReset(msg))
                    Reset();

                else if (Rules.IsBackspace(msg))
                {
                    secondNumber = secondNumber.Remove(secondNumber.Length - 1);
                    changeTextDelegate.Invoke(secondNumber);

                    if (secondNumber.Length <= 0)
                        changeTextDelegate.Invoke("0");
                }

                else if (Rules.IsMemoryOperation(msg))
                    MemorySave(msg, true);

                else if (Rules.IsSinCos(msg))
                {
                    SinCos(msg, true);
                }

                else if (msg == "Mod")
                    Modulo(msg, true);

                else if (msg == "log")
                    Logarithm(msg, true);

                else if (msg == "x^y")
                    ParticularPower(msg, true);

                else if (msg == "10^x")
                    PowerOfTen(msg, true);

                else if (msg == "n!")
                    Factorial(msg, true);

                else if (msg == "yrootx")
                    AnyRoot(msg, true);

                else if (msg == ")")
                {
                    reccuring.Add(secondNumber);
                    reccuring.Add(")");
                    string text = string.Join(" ", reccuring.ToArray());
                    changeRecurringTextDelegate.Invoke(text);
                    Expression(msg, true);
                }
            }
        }

        void Operation(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Operation;

                if (operation.Length != 0 && afterResult == false)
                {
                    isExpression = true;
                    if ((msg == "x" || msg == "÷") && (operations.Peek() == "+" || operations.Peek() == "-"))
                    {
                        operation = msg;
                        operations.Push(operation);
                        firstNumber = secondNumber;
                        reccuring.Add(secondNumber);
                        secondNumber = "";
                        precendence = int.Parse(firstNumber);
                    }

                    else if ((msg == "x" || msg == "÷") && (operations.Peek() == "x" || operations.Peek() == "÷"))
                    {
                        if (operation == "x")
                            precendence *= int.Parse(secondNumber);
                        else if (operation == "÷")
                            precendence /= int.Parse(secondNumber);
                        changeTextDelegate.Invoke(precendence.ToString());
                        operation = msg;
                        operations.Push(operation);
                        reccuring.Add(secondNumber);
                        secondNumber = "";
                    }

                    else if ((msg == "+" || msg == "-") && (operations.Peek() == "x" || operations.Peek() == "÷"))
                    {
                        if (operation == "x")
                            precendence *= int.Parse(secondNumber);
                        else if (operation == "÷")
                            precendence /= int.Parse(secondNumber);
                        if (signs.Count > 0 && signs.Peek() == "-")
                            precendence *= (-1);
                        sumNumbers.Push(precendence);
                        Stack<int> stack = new Stack<int>();
                        int temp = 0;
                        while (sumNumbers.Count > 0)
                        {
                            temp += sumNumbers.Peek();
                            stack.Push(sumNumbers.Peek());
                            sumNumbers.Pop();
                        }
                        while (stack.Count > 0)
                        {
                            sumNumbers.Push(stack.Peek());
                            stack.Pop();
                        }
                        changeTextDelegate.Invoke(temp.ToString());
                        precendence = 0;
                        operation = msg;
                        operations.Push(operation);
                        signs.Push(operation);
                        reccuring.Add(secondNumber);
                        secondNumber = "";
                    }

                    else if ((msg == "+" || msg == "-") && (operations.Peek() == "-" || operations.Peek() == "-"))
                    {
                        if (operation == "-")
                            sumNumbers.Push(int.Parse(secondNumber) * (-1));
                        else if (operation == "+")
                            sumNumbers.Push(int.Parse(secondNumber));
                        operation = msg;
                        operations.Push(operation);
                        signs.Push(operation);
                        reccuring.Add(secondNumber);
                        secondNumber = "";
                    }
                }

                else if (operation.Length != 0 && afterResult == true)
                {
                    operation = msg;
                    firstNumber = resultNumber;
                    reccuring.Add(secondNumber);
                    secondNumber = "";
                    afterResult = false;
                }

                else
                {
                    operation = msg;
                    operations.Push(operation);
                    if (operation == "+" || operation == "-")
                    {
                        sumNumbers.Push(int.Parse(secondNumber));
                        signs.Push(operation);
                    }
                    else if (operation == "x" || operation == "÷")
                        precendence = int.Parse(secondNumber);

                    reccuring.Add(secondNumber);
                    if (firstNumber == "")
                        firstNumber = secondNumber;
                    //reccuring.Add(firstNumber);
                    secondNumber = "";
                }

                string text = string.Join(" ", reccuring.ToArray());
                changeRecurringTextDelegate.Invoke(text);
            }

            else
            {
                if (Rules.IsDigit(msg))
                    AccumulateDigits(msg, true);

                else if (Rules.IsResult(msg))
                {
                    secondNumber = firstNumber;
                    Result(msg, true);
                }

                else if (Rules.IsFullReset(msg))
                    FullReset();

                else if (Rules.IsReset(msg))
                    Reset();

                else if (Rules.IsMemoryOperation(msg))
                    MemorySave(msg, true);

                else if (msg == "(")
                {
                    reccuring.Add("(");
                    string text = string.Join(" ", reccuring.ToArray());
                    changeRecurringTextDelegate.Invoke(text);
                    Expression(msg, true);
                }
            }
        }

        void Expression(string msg, bool isInput)
        {
            calcState = CalcState.Expression;
            isAdvancedExpression = true;
            isExpression = false;
            Stack<Stack<int>> allSumNumbers = new Stack<Stack<int>>();
            Stack<Stack<string>> allOperations = new Stack<Stack<string>>();
            Stack<Stack<string>> allSigns = new Stack<Stack<string>>();
            Stack<int> precendenceNumbers = new Stack<int>();

            if (isInput)
            {
                if (msg == "(")
                {
                    operation = "";
                    secondNumber = "";
                    allSumNumbers.Push(sumNumbers);
                    allOperations.Push(operations);
                    allSigns.Push(signs);
                    precendenceNumbers.Push(precendence);
                    sumNumbers = new Stack<int>();
                    operations = new Stack<string>();
                    signs = new Stack<string>();
                    precendence = 0;
                }

                else if (msg == ")")
                {
                    if (operations.Peek() == "+")
                        sumNumbers.Push(int.Parse(secondNumber));
                    else if (operations.Peek() == "-")
                        sumNumbers.Push(int.Parse(secondNumber) * (-1));
                    else if (operations.Peek() == "x")
                    {
                        if (signs.Peek() == "-")
                            precendence *= (-1);
                        precendence *= int.Parse(secondNumber);
                        sumNumbers.Push(precendence);
                    }
                    else if (operations.Peek() == "÷")
                    {
                        if (signs.Peek() == "-")
                            precendence *= (-1);
                        precendence /= int.Parse(secondNumber);
                        sumNumbers.Push(precendence);
                    }

                    int temp = 0;
                    while (sumNumbers.Count > 0)
                    {
                        temp += sumNumbers.Peek();
                        sumNumbers.Pop();
                    }
                    secondNumber = temp.ToString();
                    changeTextDelegate.Invoke(secondNumber);
                    allSumNumbers.Push(sumNumbers);
                    allOperations.Push(operations);
                    allSigns.Push(signs);
                    precendenceNumbers.Push(precendence);
                    sumNumbers = allSumNumbers.Peek();
                    operations = allOperations.Peek();
                    signs = allSigns.Peek();
                    precendence = precendenceNumbers.Peek();
                }
            }

            else
            {
                if (Rules.IsNonZeroDigit(msg))
                    AccumulateDigits(msg, true);
                else if (Rules.IsResult(msg))
                    Result(msg, true);
            }
        }

        void Result(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Result;

                if (operation == "Mod")
                {
                    resultNumber = (int.Parse(firstNumber) % int.Parse(secondNumber)).ToString();
                }

                else if (operation == "pow")
                {
                    resultNumber = Math.Pow(double.Parse(firstNumber), double.Parse(secondNumber)).ToString();
                }

                else if (operation == "root")
                {
                    resultNumber = Math.Pow(double.Parse(firstNumber), 1.0 / double.Parse(secondNumber)).ToString();
                }

                else if (isExpression == true || isAdvancedExpression == true)
                {
                    if (operations.Peek() == "+")
                        sumNumbers.Push(int.Parse(secondNumber));
                    else if (operations.Peek() == "-")
                        sumNumbers.Push(int.Parse(secondNumber) * (-1));
                    else if (operations.Peek() == "x")
                    {
                        if (signs.Peek() == "-")
                            precendence *= (-1);
                        precendence *= int.Parse(secondNumber);
                        sumNumbers.Push(precendence);
                    }
                    else if (operations.Peek() == "÷")
                    {
                        if (signs.Peek() == "-")
                            precendence *= (-1);
                        precendence /= int.Parse(secondNumber);
                        sumNumbers.Push(precendence);
                    }

                    int temp = 0;
                    while (sumNumbers.Count > 0)
                    {
                        temp += sumNumbers.Peek();
                        sumNumbers.Pop();
                    }
                    resultNumber = temp.ToString();
                }

                /*else if (isAdvancedExpression == true)
                {

                }*/

                else
                    resultNumber = PerformCalculation();

                changeTextDelegate.Invoke(resultNumber);
                firstNumber = resultNumber;
                isExpression = false;
                isAdvancedExpression = false;

                reccuring = new List<string>();
                changeRecurringTextDelegate.Invoke("");
            }

            else
            {
                if (Rules.IsOperation(msg))
                {
                    afterResult = true;
                    Operation(msg, true);
                }

                else if (Rules.IsDigit(msg))
                    AccumulateDigits(msg, true);

                else if (Rules.IsResult(msg))
                    Result(msg, true);

                else if (Rules.IsMemoryOperation(msg))
                {
                    memoryNumberToFirst = true;
                    resultToMemory = true;
                    MemorySave(msg, true);
                }

                else if (Rules.IsFullReset(msg))
                    FullReset();

                else if (Rules.IsReset(msg))
                    Reset();
            }
        }

        void MemorySave(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.MemorySave;

                double result = double.Parse(memoryNumber.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture); ;
                double num;

                if (secondNumber == "")
                    secondNumber = "0";

                if (resultToMemory)
                    num = double.Parse(resultNumber.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                else
                    num = double.Parse(secondNumber.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);

                if (msg == "M+")
                {
                    result += num;
                    memoryNumber = result.ToString();
                    memoryBox.Add(memoryNumber);
                }

                else if (msg == "M-")
                {
                    result -= num;
                    memoryNumber = result.ToString();
                    memoryBox.Add(memoryNumber);
                }

                else if (msg == "MS")
                    memoryNumber = num.ToString();

                else if (msg == "MR")
                {
                    if (memoryNumberToFirst)
                    {
                        operation = "";
                        firstNumber = memoryNumber;
                        changeTextDelegate.Invoke(firstNumber);
                    }
                    else
                    {
                        secondNumber = memoryNumber;
                        changeTextDelegate.Invoke(secondNumber);
                    }
                }

                else if (msg == "MC")
                {
                    memoryNumber = "0";
                    memoryBox = new List<string>();
                }

                memoryNumberToFirst = false;
            }

            else
            {
                if (Rules.IsDigit(msg))
                {
                    FullReset();
                    AccumulateDigits(msg, true);
                }

                else if (Rules.IsMemoryOperation(msg))
                    MemorySave(msg, true);

                else if (Rules.IsOperation(msg))
                    Operation(msg, true);

                else if (Rules.IsResult(msg))
                    Result(msg, true);

                else if (Rules.IsFullReset(msg))
                    FullReset();
            }

            resultToMemory = false;
        }

        void SinCos(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.SinCos;

                double num = double.Parse(secondNumber, System.Globalization.CultureInfo.InvariantCulture);
                num = num * Math.PI / 180;
                double result = 0;

                if (msg == "sin")
                {
                    result = Math.Sin(num);
                }

                else if (msg == "cos")
                {
                    result = Math.Cos(num);
                }

                resultNumber = result.ToString();
                changeTextDelegate.Invoke(resultNumber);
            }
        }

        void Modulo(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Modulo;
                operation = "Mod";
                firstNumber = secondNumber;
                secondNumber = "";
            }

            else
            {
                if (Rules.IsNonZeroDigit(msg))
                    AccumulateDigits(msg, true);
            }
        }

        void Logarithm(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Logarithm;
                double num = double.Parse(secondNumber, System.Globalization.CultureInfo.InvariantCulture);
                double result = 0;
                result = Math.Log10(num);
                resultNumber = result.ToString();
                changeTextDelegate.Invoke(resultNumber);
            }
        }

        void ParticularPower(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.ParticularPower;
                operation = "pow";
                firstNumber = secondNumber;
                secondNumber = "";
            }

            else
            {
                if (Rules.IsNonZeroDigit(msg))
                {
                    AccumulateDigits(msg, true);
                }
            }
        }

        void PowerOfTen(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.PowerOfTen;
                double num = double.Parse(secondNumber, System.Globalization.CultureInfo.InvariantCulture);
                double result = 0;
                result = Math.Pow(10, num);
                resultNumber = result.ToString();
                changeTextDelegate.Invoke(resultNumber);
            }
        }

        void Factorial(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Factorial;

                int num = int.Parse(secondNumber);
                long result = 1;
                for (int i = 1; i <= num; i++)
                {
                    result *= i;
                }
                resultNumber = result.ToString();
                changeTextDelegate.Invoke(resultNumber);
            }
        }

        void AnyRoot(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.AnyRoot;
                operation = "root";
                firstNumber = secondNumber;
                secondNumber = "";
            }

            else
            {
                if (Rules.IsNonZeroDigit(msg))
                {
                    AccumulateDigits(msg, true);
                }
            }
        }

        string PerformCalculation()
        {
            double first = double.Parse(firstNumber.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            double second = double.Parse(secondNumber.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);

            if (operation == "+")
                return (first + second).ToString();

            else if (operation == "-")
                return (first - second).ToString();

            else if (operation == "x")
                return (first * second).ToString();

            else if (operation == "÷")
                return (first / second).ToString();

            else if (operation == "x^2")
                return (first * second).ToString();

            else if (operation == "√")
                return Math.Sqrt(first).ToString();

            else if (operation == "1/x")
                return (1 / first).ToString();

            return "";
        }

        void CalculatePercent()
        {
            double first = double.Parse(firstNumber.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            double second = double.Parse(secondNumber.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            double result = first / 100 * second;
            secondNumber = result.ToString();
        }

        void CalculateSecondPercent()
        {
            double second = double.Parse(secondNumber.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            double result = second / 100;
            secondNumber = result.ToString();
        }

        void FullReset()
        {
            calcState = CalcState.Zero;
            firstNumber = "";
            secondNumber = "";
            resultNumber = "";
            operation = "";
            afterResult = false;
            reccuring = new List<string>();
            changeTextDelegate.Invoke("0");
            changeRecurringTextDelegate.Invoke("");
        }

        void Reset()
        {
            secondNumber = "";
            resultNumber = "";
            changeTextDelegate.Invoke("0");
        }
    }
}