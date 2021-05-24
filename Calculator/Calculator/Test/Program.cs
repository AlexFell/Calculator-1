using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        public const double PI = 3.1415926535897931;
        public const double E = 2.71828182846;


        static readonly string[] zeroPriority = { "e", "π" };
        static readonly string[] firstPriority = { "√", "ln", "lg", "cos", "sin", "tg", "ctg", "arcctg", "arctg", "arccos", "arcsin" };
        static readonly string[] secondPriority = { "!", "%" };
        static readonly string[] thirdPriority = { "^" };
        static readonly string[] fourthPriority = { "*", "/" };
        static readonly string[] fifthPriority = { "+", "-" };



        static Dictionary<int, string[]> operators = new Dictionary<int, string[]>()
        {
            {0, zeroPriority },
            {1, firstPriority },
            {2, secondPriority },
            {3, thirdPriority },
            {4, fourthPriority },
            {5, fifthPriority },
        };



        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string toCalculate = "(" + input + ")";
            double answer = Calculation(toCalculate);
            Console.WriteLine(answer);
            Console.WriteLine(Math.Cos(24));
            Console.ReadKey();
        }

        public static double Fact(double operand)
        {
            if (operand == 0)
                return 1;
            else
                return operand * Fact(operand - 1);
        }

        static public double Calculation(string mainString)
        {
            int subStringBegin = mainString.LastIndexOf("(") + 1;
            int subStringEnd = mainString.IndexOf(")");
            string subString = mainString[subStringBegin..subStringEnd];
            double result;

            while (Double.TryParse(subString, out result) == false)
            {
                for (int actualOperatorKey = 0; actualOperatorKey < operators.Count; actualOperatorKey++)
                {
                    string verifyingOperator = "e";
                    int actualOperatorPosition = (actualOperatorKey < 2 || actualOperatorKey == 3) ? -1 : subString.Length;
                    foreach (string item in operators[actualOperatorKey])
                    {
                        if ((actualOperatorKey == 2 || actualOperatorKey > 3) && actualOperatorPosition > subString.IndexOf(item) && subString.IndexOf(item) != -1)
                        {
                            verifyingOperator = item;
                            actualOperatorPosition = subString.IndexOf(item);
                        }
                        else
                        {
                            if ((actualOperatorKey < 2 || actualOperatorKey == 3) && actualOperatorPosition < subString.LastIndexOf(item) && subString.LastIndexOf(item) != -1)
                            {
                                verifyingOperator = item;
                                actualOperatorPosition = subString.LastIndexOf(item);
                            }
                        }
                    }
                    if (actualOperatorPosition != -1) { subString = OperatorTransform(subString, actualOperatorPosition, actualOperatorKey, verifyingOperator); }
                }
            }
            return result;
        }






        static public string OperatorTransform(string subString, int operatorPosition, int operatorKey, string operatorValue)
        {
            int leftOperandBegin = LeftwardOperatorSearch(subString, operatorPosition, operatorKey) + 1;
            int leftOperandEnd = operatorPosition;
            int rightOperandBegin = operatorPosition + operatorValue.Length;
            int rightOperandEnd = (RightwardOperatorSearch(subString, operatorPosition, operatorKey) == subString.Length) ? subString.Length : operatorPosition + RightwardOperatorSearch(subString, operatorPosition, operatorKey);
            switch (operatorKey)
            {
                case 0:
                    {
                        subString = (subString[operatorPosition].ToString() == "e") ? subString.Replace(subString[operatorPosition].ToString(), "*" + E.ToString() + "*") : subString.Replace(subString[operatorPosition].ToString(), "*" + PI.ToString() + "*");
                    }
                    break;

                case 1:
                    {
                        switch (operatorValue)
                        {
                            case "cos":
                                {
                                    if (rightOperandEnd == rightOperandBegin)
                                    {
                                        subString = subString.Insert(rightOperandBegin, "0");
                                        rightOperandEnd += 1;
                                    }
                                    subString = subString.Replace(subString[operatorPosition..rightOperandEnd], Math.Cos(Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString());
                                }
                                break;

                            case "sin":
                                {
                                    if (rightOperandEnd == rightOperandBegin)
                                    {
                                        subString = subString.Insert(rightOperandBegin, "90");
                                        rightOperandEnd += 2;
                                    }
                                    subString = subString.Replace(subString[operatorPosition..rightOperandEnd], Math.Sin(Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString());
                                }
                                break;

                            case "tg":
                                {
                                    if (rightOperandEnd == rightOperandBegin)
                                    {
                                        subString = subString.Insert(rightOperandBegin, "45");
                                        rightOperandEnd += 2;
                                    }
                                    subString = subString.Replace(subString[operatorPosition..rightOperandEnd], Math.Tan(Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString());
                                }
                                break;

                            case "ctg":
                                {
                                    if (rightOperandEnd == rightOperandBegin)
                                    {
                                        subString = subString.Insert(rightOperandBegin, "45");
                                        rightOperandEnd += 2;
                                    }
                                    subString = subString.Replace(subString[operatorPosition..rightOperandEnd], (1 / Math.Tan(Double.Parse(subString[rightOperandBegin..rightOperandEnd]))).ToString());
                                }
                                break;

                            case "arcsin":
                                {
                                    if (rightOperandEnd == rightOperandBegin)
                                    {
                                        subString = subString.Insert(rightOperandBegin, Math.Sin(1).ToString());
                                        rightOperandEnd += Math.Sin(1).ToString().Length;
                                    }
                                    subString = subString.Replace(subString[operatorPosition..rightOperandEnd], Math.Asin(Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString());
                                }
                                break;

                            case "arccos":
                                {
                                    if (rightOperandEnd == rightOperandBegin)
                                    {
                                        subString = subString.Insert(rightOperandBegin, Math.Cos(1).ToString());
                                        rightOperandEnd += Math.Cos(1).ToString().Length;
                                    }
                                    subString = subString.Replace(subString[operatorPosition..rightOperandEnd], Math.Acos(Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString());
                                }
                                break;

                            case "arctg":
                                {
                                    if (rightOperandEnd == rightOperandBegin)
                                    {
                                        subString = subString.Insert(rightOperandBegin, Math.Tan(1).ToString());
                                        rightOperandEnd += Math.Tan(1).ToString().Length;
                                    }
                                    subString = subString.Replace(subString[operatorPosition..rightOperandEnd], Math.Atan(Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString());
                                }
                                break;

                            case "arcctg":
                                {
                                    if (rightOperandEnd == rightOperandBegin)
                                    {
                                        subString = subString.Insert(rightOperandBegin, (1 / Math.Tan(1)).ToString());
                                        rightOperandEnd += (1 / Math.Tan(1)).ToString().Length;
                                    }
                                    subString = subString.Replace(subString[operatorPosition..rightOperandEnd], (1 / Math.Atan(Double.Parse(subString[rightOperandBegin..rightOperandEnd]))).ToString());
                                }
                                break;

                            case "ln":
                                {
                                    if (rightOperandEnd == rightOperandBegin)
                                    {
                                        subString = subString.Insert(rightOperandBegin, E.ToString());
                                        rightOperandEnd += E.ToString().Length;
                                    }
                                    subString = subString.Replace(subString[operatorPosition..rightOperandEnd], Math.Log(Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString());
                                }
                                break;

                            case "lg":
                                {
                                    if (rightOperandEnd == rightOperandBegin)
                                    {
                                        subString = subString.Insert(rightOperandBegin, "10");
                                        rightOperandEnd += 2;
                                    }
                                    subString = subString.Replace(subString[operatorPosition..rightOperandEnd], Math.Log10(Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString());
                                }
                                break;

                            case "√":
                                {
                                    if (rightOperandEnd == rightOperandBegin)
                                    {
                                        subString = subString.Insert(rightOperandBegin, "1");
                                        rightOperandEnd += 1;
                                    }
                                    subString = subString.Replace(subString[operatorPosition..rightOperandEnd], Math.Pow(Double.Parse(subString[rightOperandBegin..rightOperandEnd]), 0.5).ToString());
                                }
                                break;
                        }
                    }
                    break;

                case 2:
                    {
                        if (leftOperandBegin == leftOperandEnd)
                        {
                            subString = (operatorValue == "%") ? subString.Insert(leftOperandBegin, "100") : subString.Insert(leftOperandBegin, "1");
                            leftOperandEnd += (operatorValue == "%") ? 3 : 1;
                            operatorPosition += (operatorValue == "%") ? 3 : 1;
                        }
                        subString = (operatorValue == "%") ? subString.Replace(subString[leftOperandBegin..operatorPosition], (Double.Parse(subString[leftOperandBegin..leftOperandEnd]) / 100).ToString()) :
                                                             subString.Replace(subString[leftOperandBegin..operatorPosition], Fact(Double.Parse(subString[leftOperandBegin..leftOperandEnd])).ToString());
                    }
                    break;

                case 3:
                    {
                        if (rightOperandEnd == rightOperandBegin)
                        {
                            subString = subString.Insert(rightOperandBegin, "1");
                            rightOperandEnd += 1;
                        }
                        if (leftOperandEnd == leftOperandBegin)
                        {
                            subString = subString.Insert(rightOperandBegin, "1");
                            rightOperandEnd += 1;
                        }
                        subString = subString.Replace(subString[leftOperandBegin..rightOperandEnd], Math.Pow(Double.Parse(subString[leftOperandBegin..leftOperandEnd]), Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString());
                    }
                    break;

                case 4:
                    {
                        if (rightOperandEnd == rightOperandBegin)
                        {
                            subString = subString.Insert(rightOperandBegin, "1");
                            rightOperandEnd += 1;
                        }
                        if (leftOperandEnd == leftOperandBegin)
                        {
                            subString = subString.Insert(rightOperandBegin, "1");
                            rightOperandEnd += 1;
                        }
                        subString = (operatorValue == "*") ?
                            subString.Replace(subString[leftOperandBegin..rightOperandEnd], (Double.Parse(subString[leftOperandBegin..leftOperandEnd]) * Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString()) :
                            subString.Replace(subString[leftOperandBegin..rightOperandEnd], (Double.Parse(subString[leftOperandBegin..leftOperandEnd]) / Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString());
                    }
                    break;

                case 5:
                    {
                        if (rightOperandEnd == rightOperandBegin)
                        {
                            subString = subString.Insert(rightOperandBegin, "0");
                            rightOperandEnd += 1;
                        }
                        if (leftOperandEnd == leftOperandBegin)
                        {
                            subString = subString.Insert(rightOperandBegin, "0");
                            rightOperandEnd += 1;
                        }
                        subString = (operatorValue == "+") ?
                            subString.Replace(subString[leftOperandBegin..rightOperandEnd], (Double.Parse(subString[leftOperandBegin..leftOperandEnd]) + Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString()) :
                            subString.Replace(subString[leftOperandBegin..rightOperandEnd], (Double.Parse(subString[leftOperandBegin..leftOperandEnd]) - Double.Parse(subString[rightOperandBegin..rightOperandEnd])).ToString());
                    }
                    break;
            }
            return subString;
        }




        static public int LeftwardOperatorSearch(string subString, int operatorPosition, int operatorKey)
        {
            subString = subString[..operatorPosition];
            int actualOperatorPosition = -1;
            for (int actualOperatorKey = operatorKey + 1; actualOperatorKey < operators.Count; actualOperatorKey++)
            {
                    foreach (string item in operators[actualOperatorKey])
                    {
                        if (actualOperatorPosition < subString.LastIndexOf(item))
                        {
                            actualOperatorPosition = subString.LastIndexOf(item);
                        }
                    }
            }
            return actualOperatorPosition;
        }

        static public int RightwardOperatorSearch(string subString, int operatorPosition, int operatorKey)
        {
            int actualOperatorPosition = subString.Length;
            subString = subString[operatorPosition..];
            
            for (int actualOperatorKey = operatorKey + 1; actualOperatorKey < operators.Count; actualOperatorKey++)
            {
                    foreach (string item in operators[actualOperatorKey])
                    {
                        if (actualOperatorPosition > subString.IndexOf(item) && subString.IndexOf(item) != -1)
                        {
                            actualOperatorPosition = subString.IndexOf(item);
                        }
                    }
            }
            return actualOperatorPosition;
        }

    }
}
