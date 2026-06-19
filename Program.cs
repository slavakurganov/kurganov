using System;
using System.IO;

namespace ReportsCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== УТИЛИТА СОЗДАНИЯ ПАПОК Reports ===");
            Console.WriteLine();

            DateTime now = DateTime.Now;

            string baseDir = "Reports";
            string yearDir = Path.Combine(baseDir, now.Year.ToString());
            string monthDir = Path.Combine(yearDir, now.Month.ToString("00"));

            Console.WriteLine($"Будет создана папка: {monthDir}");
            Console.WriteLine();

            try
            {
                Directory.CreateDirectory(monthDir);
                Console.WriteLine("[OK] Папка создана");

                string reportName = $"report_{now.Year}{now.Month:00}{now.Day:00}_{now.Hour:00}{now.Minute:00}{now.Second:00}.txt";
                string reportPath = Path.Combine(monthDir, reportName);

                string text = "ОТЧЕТ О СОЗДАНИИ СТРУКТУРЫ\n";
                text += $"Дата: {now.ToString("dd.MM.yyyy HH:mm:ss")}\n";
                text += $"Путь: {monthDir}\n";
                text += $"Год: {now.Year}\n";
                text += $"Месяц: {now.Month:00}\n";
                text += "----------------------------------------\n";
                text += "Структура папок создана успешно!";

                File.WriteAllText(reportPath, text);
                Console.WriteLine($"[OK] Отчет сохранен: {reportPath}");
                Console.WriteLine();

                Console.WriteLine("=== ДЕРЕВО ПАПОК ===");
                Console.WriteLine();

                if (Directory.Exists(baseDir))
                {
                    PrintTree(baseDir, "");
                }
                else
                {
                    Console.WriteLine("Папка Reports не найдена");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void PrintTree(string path, string indent)
        {
            try
            {
                string[] items = Directory.GetFileSystemEntries(path);

                for (int i = 0; i < items.Length; i++)
                {
                    bool isLast = (i == items.Length - 1);

                    string prefix = isLast ? "└── " : "├── ";
                    string newIndent = indent + (isLast ? "    " : "│   ");

                    string name = Path.GetFileName(items[i]);
                    bool isDir = Directory.Exists(items[i]);

                    if (isDir)
                    {
                        Console.WriteLine($"{indent}{prefix}{name}/");
                        PrintTree(items[i], newIndent);
                    }
                    else
                    {
                        Console.WriteLine($"{indent}{prefix}{name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{indent}[Ошибка доступа] {ex.Message}");
            }
        }
    }
}
