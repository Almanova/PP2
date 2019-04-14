using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Rules
    {
        public static bool IsDigit(string c)
        {
            string[] arr = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            return arr.Contains(c);
        }

        public static bool IsNonZeroDigit(string c)
        {
            string[] arr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            return arr.Contains(c);
        }

        public static bool IsZero(string c)
        {
            string[] arr = new string[] { "0" };
            return arr.Contains(c);
        }

        public static bool IsDivider(string c)
        {
            string[] arr = new string[] { "," };
            return arr.Contains(c);
        }

        public static bool IsOperation(string c)
        {
            string[] arr = new string[] { "+", "-", "x", "÷", "√", "x^2", "1/x" };
            return arr.Contains(c);
        }

        public static bool IsResult(string c)
        {
            string[] arr = new string[] { "=" };
            return arr.Contains(c);
        }

        public static bool IsReset(string c)
        {
            string[] arr = new string[] { "CE" };
            return arr.Contains(c);
        }

        public static bool IsFullReset(string c)
        {
            string[] arr = new string[] { "C" };
            return arr.Contains(c);
        }

        public static bool IsBackspace(string c)
        {
            string[] arr = new string[] { "<<" };
            return arr.Contains(c);
        }

        public static bool IsSeparator(string c)
        {
            string[] arr = new string[] { "," };
            return arr.Contains(c);
        }

        public static bool IsChangingSign(string c)
        {
            string[] arr = new string[] { "+-" };
            return arr.Contains(c);
        }

        public static bool IsPercent(string c)
        {
            string[] arr = new string[] { "%" };
            return arr.Contains(c);
        }

        public static bool IsMemoryOperation(string c)
        {
            string[] arr = new string[] { "MC", "MR", "M+", "M-", "MS" };
            return arr.Contains(c);
        }
    }
}
