using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    class StringCalculation
    {
        public const double PI = 3.1415926535897931;
        public const double E = 2.71828182846;

        Dictionary<string, string> operators = new Dictionary<string, string>()
        {
            {"0", "e" },
            {"0", "π" },
            {"1", "√" },
            {"1", "ln" },
            {"1", "lg" },
            {"1", "cos" },
            {"1", "sin" },
            {"1", "tg" },
            {"1", "ctg" },
            {"1", "arcsin" },
            {"1", "arccos" },
            {"1", "arctg" },
            {"1", "arcctg" },
            {"2", "!" },
            {"2", "%" },
            {"3", "^" },
            {"4", "*" },
            {"4", "/" },
            {"5", "+" },
            {"5", "-" },
        };

        public static long Fact(long operand)
        {
            if (operand == 0)
                return 1;
            else
                return operand * Fact(operand - 1);
        }

        public double Calculation(string mainString)
        {
            int subStringBegin = mainString.LastIndexOf("(") + 1;
            int subStringEnd = mainString.IndexOf(")") - 1;
            string subString = mainString.Substring(subStringBegin).Remove(subStringEnd);
            if (long.TryParse(subString, out long result))
            { }
            else
            {
                for (int actualOperatorKey = 0 ; actualOperatorKey < 5; actualOperatorKey++)
                {
                    while (subString.Contains(operators[actualOperatorKey.ToString()]))
                    {
                        int actualOperatorPosition = (actualOperatorKey < 2) ? subString.LastIndexOf(operators[actualOperatorKey.ToString()]) : subString.IndexOf(operators[actualOperatorKey.ToString()]);
                        string operatorValue = operators[actualOperatorKey.ToString()];
                        foreach (KeyValuePair<string, string> pair in operators)
                        {
                            if (actualOperatorKey < 2)
                            {
                                if (actualOperatorPosition < subString.LastIndexOf(pair.Value) && pair.Key == actualOperatorKey.ToString())
                                {
                                    actualOperatorPosition = subString.LastIndexOf(pair.Value);
                                }
                            }
                            else
                            {
                                if (actualOperatorPosition > subString.IndexOf(pair.Value) && pair.Key == actualOperatorKey.ToString())
                                {
                                    actualOperatorPosition = subString.IndexOf(pair.Value);
                                    operatorValue = pair.Value;
                                }
                            }
                        }
                        subString = OperatorTransform(subString, actualOperatorPosition, actualOperatorKey, operatorValue);
                    }
                }
            }
            return result;
        }

        public int OperatorSearch(string subString, int operatorPosition, int operatorKey)
        {
            int actualOperatorPosition = subString.Length;
            for (int actualOperatorKey = operatorKey; actualOperatorKey < 5; actualOperatorKey++)
            {
                actualOperatorPosition = subString.IndexOf(operators[actualOperatorKey.ToString()]);
                string operatorValue = operators[actualOperatorKey.ToString()];
                foreach (KeyValuePair<string, string> pair in operators)
                {
                    if (actualOperatorPosition > subString.IndexOf(pair.Value) && pair.Key == actualOperatorKey.ToString() && subString.IndexOf(pair.Value) > operatorPosition && operatorKey < 2)
                    {
                        actualOperatorPosition = subString.IndexOf(pair.Value);
                    }
                    else if (actualOperatorPosition < subString.IndexOf(pair.Value) && pair.Key == actualOperatorKey.ToString() && subString.IndexOf(pair.Value) < operatorPosition && operatorKey == 2)
                    {
                        actualOperatorPosition = subString.IndexOf(pair.Value);
                    }
                }
            }
            return actualOperatorPosition;
        }

        public string OperatorTransform(string subString, int operatorPosition, int operatorKey, string operatorValue)
        {
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
                                    int operandBegin = operatorPosition + 3;
                                    int nextOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                                    int operandLenght = nextOperatorPosition - operandBegin;
                                    if (operandLenght == 0)
                                    {
                                        operandLenght = 1;
                                        subString += "0";
                                    }
                                    subString = subString.Replace(subString.Substring(operatorPosition).Remove(nextOperatorPosition).ToString(), Math.Cos(long.Parse(subString.Substring(operandBegin, operandLenght))).ToString());
                                }
                                break;
                            case "sin":
                                {
                                    int operandBegin = operatorPosition + 3;
                                    int nextOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                                    int operandLenght = nextOperatorPosition - operandBegin;
                                    if (operandLenght == 0)
                                    {
                                        operandLenght = 2;
                                        subString += "90";
                                    }
                                    subString = subString.Replace(subString.Substring(operatorPosition).Remove(nextOperatorPosition).ToString(), Math.Sin(long.Parse(subString.Substring(operandBegin, operandLenght))).ToString());
                                }
                                break;
                            case "tg":
                                {
                                    int operandBegin = operatorPosition + 2;
                                    int nextOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                                    int operandLenght = nextOperatorPosition - operandBegin;
                                    if (operandLenght == 0)
                                    {
                                        operandLenght = 2;
                                        subString += "45";
                                    }
                                    subString = subString.Replace(subString.Substring(operatorPosition).Remove(nextOperatorPosition).ToString(), Math.Tan(long.Parse(subString.Substring(operandBegin, operandLenght))).ToString());
                                }
                                break;
                            case "ctg":
                                {
                                    int operandBegin = operatorPosition + 3;
                                    int nextOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                                    int operandLenght = nextOperatorPosition - operandBegin;
                                    if (operandLenght == 0)
                                    {
                                        operandLenght = 2;
                                        subString += "45";
                                    }
                                    subString = subString.Replace(subString.Substring(operatorPosition).Remove(nextOperatorPosition).ToString(), (1 / Math.Tan(long.Parse(subString.Substring(operandBegin, operandLenght)))).ToString());
                                }
                                break;
                            case "arcsin":
                                {
                                    int operandBegin = operatorPosition + 6;
                                    int nextOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                                    int operandLenght = nextOperatorPosition - operandBegin;
                                    if (operandLenght == 0)
                                    {
                                        operandLenght = Math.Sin(1).ToString().Length;
                                        subString += Math.Sin(1).ToString();
                                    }
                                    subString = subString.Replace(subString.Substring(operatorPosition).Remove(nextOperatorPosition).ToString(), Math.Asin(long.Parse(subString.Substring(operandBegin, operandLenght))).ToString());
                                }
                                break;
                            case "arccos":
                                {
                                    int operandBegin = operatorPosition + 6;
                                    int nextOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                                    int operandLenght = nextOperatorPosition - operandBegin;
                                    if (operandLenght == 0)
                                    {
                                        operandLenght = Math.Cos(1).ToString().Length;
                                        subString += Math.Cos(1).ToString();
                                    }
                                    subString = subString.Replace(subString.Substring(operatorPosition).Remove(nextOperatorPosition).ToString(), Math.Acos(long.Parse(subString.Substring(operandBegin, operandLenght))).ToString());
                                }
                                break;
                            case "arctg":
                                {
                                    int operandBegin = operatorPosition + 6;
                                    int nextOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                                    int operandLenght = nextOperatorPosition - operandBegin;
                                    if (operandLenght == 0)
                                    {
                                        operandLenght = Math.Tan(1).ToString().Length;
                                        subString += Math.Tan(1).ToString();
                                    }
                                    subString = subString.Replace(subString.Substring(operatorPosition).Remove(nextOperatorPosition).ToString(), Math.Atan(long.Parse(subString.Substring(operandBegin, operandLenght))).ToString());
                                }
                                break;
                            case "arcctg":
                                {
                                    int operandBegin = operatorPosition + 6;
                                    int nextOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                                    int operandLenght = nextOperatorPosition - operandBegin;
                                    if (operandLenght == 0)
                                    {
                                        operandLenght = (1 / Math.Tan(1)).ToString().Length;
                                        subString += (1 / Math.Tan(1)).ToString();
                                    }
                                    subString = subString.Replace(subString.Substring(operatorPosition).Remove(nextOperatorPosition).ToString(), (1 / Math.Atan(long.Parse(subString.Substring(operandBegin, operandLenght)))).ToString());
                                }
                                break;
                            case "ln":
                                {
                                    int operandBegin = operatorPosition + 3;
                                    int nextOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                                    int operandLenght = nextOperatorPosition - operandBegin;
                                    if (operandLenght == 0)
                                    {
                                        operandLenght = E.ToString().Length;
                                        subString += E.ToString();
                                    }
                                    subString = subString.Replace(subString.Substring(operatorPosition).Remove(nextOperatorPosition).ToString(), Math.Log(long.Parse(subString.Substring(operandBegin, operandLenght))).ToString());
                                }
                                break;
                            case "lg":
                                {
                                    int operandBegin = operatorPosition + 3;
                                    int nextOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                                    int operandLenght = nextOperatorPosition - operandBegin;
                                    if (operandLenght == 0)
                                    {
                                        operandLenght = 2;
                                        subString += "10";
                                    }
                                    subString = subString.Replace(subString.Substring(operatorPosition).Remove(nextOperatorPosition).ToString(), Math.Log10(long.Parse(subString.Substring(operandBegin, operandLenght))).ToString());
                                }
                                break;
                            case "√":
                                {
                                    int operandBegin = operatorPosition + 3;
                                    int nextOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                                    int operandLenght = nextOperatorPosition - operandBegin;
                                    if (operandLenght == 0)
                                    {
                                        operandLenght = 1;
                                        subString += "1";
                                    }
                                    subString = subString.Replace(subString.Substring(operatorPosition).Remove(nextOperatorPosition).ToString(), Math.Pow(long.Parse(subString.Substring(operandBegin, operandLenght)), 0.5).ToString());
                                }
                                break;
                        }
                    }
                    break;

                case 2:
                    {
                        int operandEnd = operatorPosition - 1;
                        int prevOperatorPosition = OperatorSearch(subString, operatorPosition, operatorKey + 1);
                        int operandLenght = operandEnd - prevOperatorPosition;
                        if (operandLenght == 0 && operatorValue == "!" )
                        {
                            operandLenght = 1;
                            subString += "1";
                        }
                        else if (operandLenght == 0 && operatorValue == "%")
                        {
                            operandLenght = 3;
                            subString += "100";
                        }
                        subString = (operatorValue == "%") ? subString.Replace(subString.Substring(prevOperatorPosition + 1).Remove(operatorPosition + 1).ToString(),
                            (long.Parse(subString.Substring(prevOperatorPosition + 1, operandLenght)) / 100).ToString()) :
                                                             subString.Replace(subString.Substring(prevOperatorPosition + 1).Remove(operatorPosition + 1).ToString(),
                            Fact(long.Parse(subString.Substring(prevOperatorPosition + 1, operandLenght))).ToString());
                    }
                    break;
            }
            return subString;
        }
    }
}
