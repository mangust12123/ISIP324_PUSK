using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr1
{
    class Expense
    {
        public string Name;
        public double Amount;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            int count = 0;
            while (true)
            {
                Console.Write("Введите количество трат (от 2 до 40): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out count) && count >= 2 && count <= 40)
                    break;

                Console.WriteLine("Ошибка! Число должно быть от 2 до 40. Попробуйте снова.\n");
            }

            Expense[] expenses = new Expense[count];
            Console.WriteLine("\nВведите траты по шаблону: Название; Сумма");
            Console.WriteLine("Пример: Влажные салфетки \"Лента\"; 235");
            Console.WriteLine("-----------------------------------------");

            for (int i = 0; i < count; i++)
            {
                Console.Write($"Трата {i + 1}: ");
                string line = Console.ReadLine();
                string[] parts = line.Split(';');

                if (parts.Length == 2 && double.TryParse(parts[1].Trim(), out double amount))
                {
                    expenses[i] = new Expense
                    {
                        Name = parts[0].Trim(),
                        Amount = amount
                    };
                }
                else
                {
                    Console.WriteLine("Неверный формат! Записываем с суммой 0.");
                    expenses[i] = new Expense
                    {
                        Name = line,
                        Amount = 0
                    };
                }
            }
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n====== МЕНЮ ======");
                Console.WriteLine("1. Вывод данных");
                Console.WriteLine("2. Статистика");
                Console.WriteLine("3. Сортировка по цене (пузырьком)");
                Console.WriteLine("4. Конвертация валюты");
                Console.WriteLine("5. Поиск по названию");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт: ");

                string choiceStr = Console.ReadLine();

                switch (choiceStr)
                {
                    case "1":
                        PrintExpenses(expenses);
                        break;

                    case "2":
                        ShowStatistics(expenses);
                        break;

                    case "3":
                        BubbleSort(expenses);
                        Console.WriteLine("\nТраты отсортированы по возрастанию!");
                        PrintExpenses(expenses);
                        break;

                    case "4":
                        ConvertCurrency(expenses);
                        break;

                    case "5":
                        SearchByName(expenses);
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Выход из программы. Удачи!");
                        break;

                    default:
                        Console.WriteLine("Нет такого пункта меню!");
                        break;
                }
            }

            Console.ReadKey();
        }
        static void PrintExpenses(Expense[] expenses)
        {
            Console.WriteLine("\n--- СПИСОК ТРАТ ---");
            for (int i = 0; i < expenses.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {expenses[i].Name} - {expenses[i].Amount} руб.");
            }
        }
        static void ShowStatistics(Expense[] expenses)
        {
            double sum = 0;
            double min = expenses[0].Amount;
            double max = expenses[0].Amount;

            for (int i = 0; i < expenses.Length; i++)
            {
                sum += expenses[i].Amount;
                if (expenses[i].Amount < min) min = expenses[i].Amount;
                if (expenses[i].Amount > max) max = expenses[i].Amount;
            }

            double avg = sum / expenses.Length;

            Console.WriteLine("\n--- СТАТИСТИКА ---");
            Console.WriteLine($"Общая сумма: {sum} руб.");
            Console.WriteLine($"Средний чек: {avg:F2} руб.");
            Console.WriteLine($"Максимальная трата: {max} руб.");
            Console.WriteLine($"Минимальная трата: {min} руб.");
        }
        static void BubbleSort(Expense[] expenses)
        {
            for (int i = 0; i < expenses.Length - 1; i++)
            {
                for (int j = 0; j < expenses.Length - i - 1; j++)
                {
                    if (expenses[j].Amount > expenses[j + 1].Amount)
                    {
                        Expense temp = expenses[j];
                        expenses[j] = expenses[j + 1];
                        expenses[j + 1] = temp;
                    }
                }
            }
        }
        static void ConvertCurrency(Expense[] expenses)
        {
            double sum = 0;
            for (int i = 0; i < expenses.Length; i++)
                sum += expenses[i].Amount;

            Console.WriteLine("\n--- КОНВЕРТАЦИЯ ---");
            Console.WriteLine($"Ваша сумма: {sum} руб.");
            Console.WriteLine("Выберите валюту:");
            Console.WriteLine("1. Доллар (курс 92.5)");
            Console.WriteLine("2. Евро (курс 100.2)");
            Console.WriteLine("3. Юань (курс 12.8)");
            Console.WriteLine("4. Ввести свой курс");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();
            double rate = 0;

            switch (choice)
            {
                case "1": rate = 92.5; break;
                case "2": rate = 100.2; break;
                case "3": rate = 12.8; break;
                case "4":
                    Console.Write("Введите курс для 1 рубля: ");
                    if (!double.TryParse(Console.ReadLine(), out rate))
                    {
                        Console.WriteLine("Неверный курс!");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Нет такого пункта.");
                    return;
            }

            Console.WriteLine($"Итого в выбранной валюте: {sum * rate:F2}");
        }
        static void SearchByName(Expense[] expenses)
        {
            Console.Write("\nВведите текст для поиска: ");
            string search = Console.ReadLine();

            Console.WriteLine("\n--- РЕЗУЛЬТАТЫ ПОИСКА ---");
            bool found = false;

            for (int i = 0; i < expenses.Length; i++)
            {
                if (expenses[i].Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"{expenses[i].Name} - {expenses[i].Amount} руб.");
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("Ничего не найдено.");
        }
    }
}