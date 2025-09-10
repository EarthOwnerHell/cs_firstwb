using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    internal class Program
    {
        static float memory = 0; // Память калькулятора

        static void Main(string[] args)
        {
            string continueCalculation;
            
            Console.WriteLine("Добро пожаловать в калькулятор. Вам необходимо ввести первое число, затем знак действия(+,-,*,/,%, √, x², ¹/ₓ, M+, M-, MR), которое хотите совершить и второе число.");
            
            do
            {
                float one, two, result;
                char sign;

                Console.Write("Введите первое число: ");
                one = ReadLimitedNumber();
                
                Console.Write("Введите знак действия: ");
                sign = Convert.ToChar(Console.ReadLine());
                
                Console.Write("Введите второе число: ");
                two = ReadLimitedNumber();

                switch (sign)
                {
                    case '+':
                        result = one + two;
                        Console.WriteLine("Сумма ваших чисел равна " + result);
                        break;
                    case '-':
                        result = one - two;
                        Console.WriteLine("Разность ваших чисел равна " + result);
                        break;
                    case '*':
                        result = one * two;
                        Console.WriteLine("Произведение ваших чисел равно " + result);
                        break;
                    case '/':
                        if (two == 0)
                        {
                            Console.WriteLine("Ошибка. Делитель не может быть равным нулю.");
                        }
                        else
                        {
                            result = one / two;
                            Console.WriteLine("Частное ваших чисел равна " + result);
                        }
                        break;
                    case '%':
                        result = one % two;
                        Console.WriteLine("Остаток от деления равен " + result);
                        break;
                    case '√': // Квадратный корень
                        if (one >= 0)
                        {
                            result = (float)Math.Sqrt(one);
                            Console.WriteLine("Квадратный корень из первого числа равен " + result);
                        }
                        else
                        {
                            Console.WriteLine("Ошибка. Нельзя извлечь корень из отрицательного числа.");
                        }
                        break;
                    case '²': // Квадрат числа
                        result = one * one;
                        Console.WriteLine("Квадрат первого числа равен " + result);
                        break;
                    case '¹': // Обратное значение (1/x)
                        if (one != 0)
                        {
                            result = 1 / one;
                            Console.WriteLine("Обратное значение первого числа равно " + result);
                        }
                        else
                        {
                            Console.WriteLine("Ошибка. Деление на ноль.");
                        }
                        break;
                    case 'M': // Операции с памятью
                        Console.Write("Введите дополнительный символ для операции с памятью (+, -, R): ");
                        char memoryOp = Convert.ToChar(Console.ReadLine());
                        
                        switch (memoryOp)
                        {
                            case '+':
                                memory += one;
                                Console.WriteLine("Значение добавлено в память: " + memory);
                                break;
                            case '-':
                                memory -= one;
                                Console.WriteLine("Значение вычтено из памяти: " + memory);
                                break;
                            case 'R':
                                Console.WriteLine("Текущее значение в памяти: " + memory);
                                break;
                            default:
                                Console.WriteLine("Неизвестная операция с памятью.");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Ошибка. Вы ввели неверный знак.");
                        break;
                }

                Console.Write("Хотите продолжить вычисления? (y/n): ");
                continueCalculation = Console.ReadLine();
                
            } while (continueCalculation == "y" || continueCalculation == "Y");
            
            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }

        static float ReadLimitedNumber()
        {
            while (true)
            {
                string input = Console.ReadLine();
                
                // Проверяем, что введено число
                if (!float.TryParse(input, out float number))
                {
                    Console.Write("Ошибка: Пожалуйста, введите корректное число. Попробуйте снова: ");
                    continue;
                }
                
                // Проверяем, что после запятой не более одной цифры
                if (input.Contains(",") || input.Contains("."))
                {
                    string decimalSeparator = input.Contains(",") ? "," : ".";
                    string[] parts = input.Split(decimalSeparator[0]);
                    
                    if (parts.Length > 1 && parts[1].Length > 1)
                    {
                        Console.Write("Ошибка: Разрешена только одна цифра после запятой. Попробуйте снова: ");
                        continue;
                    }
                }
                
                // Проверяем, что число в допустимом диапазоне
                if (Math.Abs(number) > 1000000)
                {
                    Console.Write("Ошибка: Число должно быть в диапазоне от -1000000 до 1000000. Попробуйте снова: ");
                    continue;
                }
                
                return number;
            }
        }
    }
}