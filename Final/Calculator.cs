using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;



namespace Final
{
    public class CalculationResult
    {
        public string InputExpression { get; set; }
        public double Result { get; set; }

        public override string ToString()
        {
            return $"Expression: {InputExpression}\nResult: {Result}\n";
        }
    }

    public static class Calculator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ласкаво просимо до калькулятора!");
            while (true)
            {
                Console.WriteLine("Введіть вираз, або 'вийти', щоб завершити:");
                string input = Console.ReadLine();

                if (input.ToLower() == "вийти")
                {
                    Console.WriteLine("До побачення!");
                    break;
                }

                try
                {
                    double result = EvaluateExpression(input);
                    Console.WriteLine("Результат: " + result + "\n");

                    var calculationResult = new CalculationResult
                    {
                        InputExpression = input,
                        Result = result
                    };

                    SerializeResult(calculationResult, "results.txt");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Помилка: " + e.Message + "\n");
                }
            }
        }

        public static void SerializeResult(CalculationResult result, string filePath)
        {
            string serializedResult = result.ToString();

            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(serializedResult);
            }
        }

        
        public static double EvaluateExpression(string expression)
        {
            expression = NormalizeExpression(expression);
            return EvaluateAdditionAndSubtraction(ref expression);
        }

        public static double EvaluateAdditionAndSubtraction(ref string expression)
        {
            double left = EvaluateMultiplicationAndDivision(ref expression);
            while (expression.Length > 0)
            {
                char op = expression[0];
                if (op != '+' && op != '-')
                    break;
                expression = expression.Substring(1);
                double right = EvaluateMultiplicationAndDivision(ref expression);
                if (op == '+')
                    left += right;
                else
                    left -= right;
            }
            return left;
        }

        public static double EvaluateMultiplicationAndDivision(ref string expression)
        {
            double left = EvaluateExponentiation(ref expression);
            while (expression.Length > 0)
            {
                char op = expression[0];
                if (op != '*' && op != '/')
                    break;
                expression = expression.Substring(1);
                double right = EvaluateExponentiation(ref expression);
                if (op == '*')
                    left *= right;
                else
                {
                    if (right == 0)
                        throw new DivideByZeroException();
                    left /= right;
                }
            }
            return left;
        }

        public static double EvaluateExponentiation(ref string expression)
        {
            double left = EvaluateTrigonometricFunctionsAndLogarithms(ref expression);
            while (expression.Length > 0)
            {
                char op = expression[0];
                if (op != '^')
                    break;
                expression = expression.Substring(1);
                double right = EvaluateTrigonometricFunctionsAndLogarithms(ref expression);
                left = Math.Pow(left, right);
            }
            return left;
        }

        public static double EvaluateTrigonometricFunctionsAndLogarithms(ref string expression)
        {
            if (expression.StartsWith("cos"))
            {
                expression = expression.Substring(3);
                return Math.Cos(EvaluateExpressionInParentheses(ref expression));
            }
            else if (expression.StartsWith("sin"))
            {
                expression = expression.Substring(3);
                return Math.Sin(EvaluateExpressionInParentheses(ref expression));
            }
            else if (expression.StartsWith("tg"))
            {
                expression = expression.Substring(2);
                return Math.Tan(EvaluateExpressionInParentheses(ref expression));
            }
            else if (expression.StartsWith("cot"))
            {
                expression = expression.Substring(3);
                return 1.0 / Math.Tan(EvaluateExpressionInParentheses(ref expression));
            }
            else if (expression.StartsWith("log"))
            {
                expression = expression.Substring(3);
                return Math.Log(EvaluateExpressionInParentheses(ref expression));
            }
            else
            {
                return EvaluateExpressionInParentheses(ref expression);
            }
        }

        public static double EvaluateExpressionInParentheses(ref string expression)
        {
            if (expression[0] == '(')
            {
                expression = expression.Substring(1);
                double result = EvaluateAdditionAndSubtraction(ref expression);
                if (expression.Length == 0 || expression[0] != ')')
                    throw new Exception("Не вистачає закриваючої дужки.");
                expression = expression.Substring(1);
                return result;
            }
            else
            {
                int endIndex = expression.IndexOfAny(new char[] { '+', '-', '*', '/', '^', '(', ')' });
                if (endIndex == -1)
                    endIndex = expression.Length;
                string valueStr = expression.Substring(0, endIndex);
                expression = expression.Substring(endIndex);
                return double.Parse(valueStr);
            }
        }

        public static string NormalizeExpression(string expression)
        {
            expression = expression.Replace(" ", "");

            expression = Regex.Replace(expression, @"(\d)([a-z(])", "$1*$2");
            expression = Regex.Replace(expression, @"([a-z)])(\d)", "$1*$2");

            return expression;
        }



    }  
}
