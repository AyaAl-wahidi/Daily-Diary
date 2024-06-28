using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiaryManager
{
    public class DailyDiary
    {
        private static string filePath = Path.Combine(Environment.CurrentDirectory, "myDiary.txt");

        public DailyDiary(string filePath)
        {
            filePath = filePath;
        }

        public string FilePath
        {
            set { filePath = value; }
            get { return filePath; }
        }

        public static void Run()
        {
            while (true)
            {
                Console.WriteLine("\n ************ Welcome To Daily Diary Application ************ ");
                Console.WriteLine(" 1. Read diary \n 2. Add Diary \n 3. Delete Diary \n 4. Count Diaries lines \n 5. Search Diaries by word \n 6. Exit");
                Console.Write("Choose an option : \n");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ReadDiary();
                        break;
                    case "2":
                        Console.WriteLine("Enter the date (YYYY-MM-DD):");
                        string date = Console.ReadLine();
                        Console.WriteLine("Enter the content:");
                        string content = Console.ReadLine();
                        AddDiary(date, content);
                        break;
                    case "3":
                        DeleteEntry();
                        break;
                    case "4":
                        CountLinesEntries();
                        break;
                    case "5":
                        SearchEntriesByWord();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public static string ReadDiary()
        {
            string text = "";
            try
            {
                if (File.Exists(filePath))
                {
                    text = File.ReadAllText(filePath);
                    Console.WriteLine(text);
                }
                else
                {
                    Console.WriteLine("File not exists!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the diary: {ex.Message}");
            }
            return text;
        }

        public static void AddDiary(string date, string content)
        {
            try
            {
                if (!DateTime.TryParse(date, out _))
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                    return;
                }

                Entry entry = new Entry { Date = date, Content = content };
                File.AppendAllText(filePath, $"\n{entry.Date} - {entry.Content}\n");
                Console.WriteLine("Entry added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entry: {ex.Message}");
            }
        }

        public static void DeleteEntry()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found.");
                    return;
                }

                Console.WriteLine("Enter the dairy you want to delete:");
                string deleteDairy = Console.ReadLine();

                Entry entries = new Entry { Content = deleteDairy };
                string text = File.ReadAllText(filePath);
                text = text.Replace(entries.Content, "");
                File.WriteAllText(filePath, text);
                Console.WriteLine("The dairy deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting entry: {e.Message}");
            }
        }

        public static void CountLinesEntries()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    int linesNumber = File.ReadAllLines(filePath).Length;
                    Console.WriteLine($"Total number of lines: {linesNumber}");
                }
                else
                {
                    Console.WriteLine("Diary file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error counting entries: {ex.Message}");
            }
        }

        public static void SearchEntriesByWord()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Diary file not found.");
                    return;
                }

                Console.WriteLine("Pleasr enter the word you search for:");
                string searchWord = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(searchWord))
                {
                    Console.WriteLine("The word you enter it is not exist! Please try again.");
                    return;
                }

                string[] fileContent = File.ReadAllLines(filePath);
                var text = fileContent.Where(txt => txt.Contains(searchWord, StringComparison.OrdinalIgnoreCase));

                if (text.Any())
                {
                    int count = 0;
                    Console.WriteLine($"Entries containing the word {searchWord}:");
                    foreach (string line in text)
                    {
                        count++;
                        Console.WriteLine($"{count}) {line}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching by word: {ex.Message}");
            }
        }
    }
}