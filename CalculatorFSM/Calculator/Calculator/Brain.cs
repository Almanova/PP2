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
        MemorySave
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
        List<string> reccuring = new List<string>();
        List<string> memoryBox = new List<string>();
        string memoryNumber = "0";

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
                    MemorySave(msg, true);
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
            }
        }

        void Operation(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Operation;

                if (operation.Length != 0 && afterResult == false)
                {
                    resultNumber = PerformCalculation();
                    changeTextDelegate.Invoke(resultNumber);
                    firstNumber = resultNumber;
                    operation = msg;
                    reccuring.Add(secondNumber);
                    secondNumber = "";
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
                    if (firstNumber == "")
                        firstNumber = secondNumber;
                    reccuring.Add(firstNumber);
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
            }
        }

        void Result(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Result;

                resultNumber = PerformCalculation();
                changeTextDelegate.Invoke(resultNumber);
                firstNumber = resultNumber;

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
            calcState = CalcState.MemorySave;

            if (isInput)
            {
                double result = double.Parse(memoryNumber.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture); ;
                double num;

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
                    memoryBox.Add(num.ToString());

                else if (msg == "MR")
                {
                    secondNumber = memoryBox[memoryBox.Count - 1];
                    changeTextDelegate.Invoke(secondNumber);
                }

                else if (msg == "MC")
                {
                    memoryNumber = "0";
                    memoryBox = new List<string>();
                }
            }

            else
            {
                if (Rules.IsDigit(msg))
                {
                    FullReset();
                    AccumulateDigits(msg, true);
                }

                else if (Rules.IsMemoryOperation(msg))
                {
                    if (msg == "MR")
                    {
                        if (memoryBox.Count == 0)
                            changeTextDelegate.Invoke("0");
                        else
                        {
                            secondNumber = memoryBox[memoryBox.Count - 1];
                            changeTextDelegate.Invoke(secondNumber);
                        }
                    }

                    else
                        MemorySave(msg, true);
                }

                else if (Rules.IsFullReset(msg))
                    FullReset();
            }

            resultToMemory = false;
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
