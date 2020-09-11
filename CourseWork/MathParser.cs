using System.Collections.Generic;
using System.Windows.Forms;
using static System.Math;

namespace CourseWork
{
    class MathParser
    {
        Dictionary<string, double> variables;

        public MathParser()
        {
            variables = new Dictionary<string, double>();
        }

        public void setVariable(string variableName, double variableValue)
        {
            if (variables.ContainsKey(variableName))
                variables[variableName] = variableValue;
            else
                variables.Add(variableName, variableValue);
        }

        public double getVariable(string variableName)
        {
            if (!variables.ContainsKey(variableName))
            {
                MessageBox.Show("Помилка! Спроба отримати не існуючу змінну '" + variableName + "'", 
                    "Помилка в ході виконання", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0.0;
            }
            return variables[variableName];
        }

        public double Parse(string str)
        {
            ResultParsing result = PlusMinus(str);
            if (result.rest != "")
            {
                MessageBox.Show("Помилка! Не можливо опрацювати рівняння. Залишок: " + result.rest,
                    "Помилка в ході виконання", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return result.acc;
        }

        private ResultParsing PlusMinus(string str)
        {
            ResultParsing current = MulDiv(str);
            double acc = current.acc;

            while (current.rest.Length > 0)
            {
                if (!(current.rest[0] == '+' || current.rest[0] == '-')) break;

                char sign = current.rest[0];
                string next = current.rest.Substring(1);

                current = MulDiv(next);
                if (sign == '+')
                {
                    acc += current.acc;
                }
                else
                {
                    acc -= current.acc;
                }
            }
            return new ResultParsing(acc, current.rest);
        }

        private ResultParsing MulDiv(string str)
        {
            ResultParsing current = Bracket(str);

            double acc = current.acc;
            while (true)
            {
                if (current.rest.Length == 0)
                {
                    return current;
                }
                char sign = current.rest[0];
                if (sign != '*' && sign != '/' && sign != '^') return current;

                string next = current.rest.Substring(1);
                ResultParsing right = Bracket(next);

                if (sign == '*')
                {
                    acc *= right.acc;
                }
                if (sign == '^')
                {
                    acc = Pow(acc, right.acc);
                }
                if (sign == '/')
                {
                    acc /= right.acc;
                }

                current = new ResultParsing(acc, right.rest);
            }
        }

        private ResultParsing Bracket(string str)
        {
            char zeroChar = str[0];
            if (zeroChar == '(')
            {
                ResultParsing r = PlusMinus(str.Substring(1));
                if (r.rest != "" && r.rest[0] == ')')
                    r.rest = r.rest.Substring(1);
                else
                    MessageBox.Show("Помилка! Не закриті дужки", 
                        "Помилка в ході виконання", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return r;
            }
            return FunctionVariable(str);
        }

        private ResultParsing FunctionVariable(string str)
        {
            string f = "";
            int i = 0;
            //Шукаєм функцію або змінну яка починаєтсья з букви
            while (i < str.Length && (char.IsLetter(str[i]) || (char.IsDigit(str[i]) && i > 0)))
            {
                f += str[i];
                i++;
            }
            if (f != "")
            {   //Якщо щось знайшли
                if (str.Length > i && str[i] == '(')
                {   //Якщо наступний символ дужка - функція
                    ResultParsing r = Bracket(str.Substring(f.Length));
                    return processFunction(f, r);
                }
                else
                {   //Змінна
                    return new ResultParsing(getVariable(f), str.Substring(f.Length));
                }
            }
            return Num(str);
        }

        private ResultParsing Num(string str)
        {
            int i = 0, dot_cnt = 0;
            bool negative = false;

            //Якщо число починається з мінуса
            if (str[0] == '-')
            {
                negative = true;
                str = str.Substring(1);
            }

            //Дозвіл на цифри і кому
            while (i < str.Length && (char.IsDigit(str[i]) || str[i] == ','))
            {
                //Дозвіл на одну кому в числі
                if (str[i] == ',' && ++dot_cnt > 1)
                {
                    MessageBox.Show("Неправильне число '" + str.Substring(0, i + 1) + "'", 
                        "Помилка в ході виконання", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                i++;
            }
            if (i == 0)
            {   //Не число
                MessageBox.Show("Помилка! Не вдається обробити число '" + str + "'",
                    "Помилка в ході виконання", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            double dPart = double.Parse(str.Substring(0, i));
            if (negative)
                dPart = -dPart;
            string restPart = str.Substring(i);

            return new ResultParsing(dPart, restPart);
        }

        private ResultParsing processFunction(string func, ResultParsing r)
        {
            switch (func)
            {
                case "sin":
                    return new ResultParsing(Sin(r.acc), r.rest);
                case "cos":
                    return new ResultParsing(Cos(r.acc), r.rest);
                case "tan":
                    return new ResultParsing(Tan(r.acc), r.rest);
                case "asin":
                    return new ResultParsing(Asin(r.acc), r.rest);
                case "acos":
                    return new ResultParsing(Acos(r.acc), r.rest);
                case "atan":
                    return new ResultParsing(Atan(r.acc), r.rest);
                case "sinh":
                    return new ResultParsing(Sinh(r.acc), r.rest);
                case "cosh":
                    return new ResultParsing(Cosh(r.acc), r.rest);
                case "sqrt":
                    return new ResultParsing(Sqrt(r.acc), r.rest);
                case "exp":
                    return new ResultParsing(Exp(r.acc), r.rest);
                case "ln":
                    return new ResultParsing(Log(r.acc), r.rest);
                case "lg":
                    return new ResultParsing(Log10(r.acc), r.rest);
                default:
                    MessageBox.Show("Функція '" + func + "' не знайдена",
                        "Помилка в ході виконання", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }

            return r;
        }
    }
}
