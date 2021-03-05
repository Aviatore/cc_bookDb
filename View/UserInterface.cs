using System;

namespace Codecool.BookDb.View
{
    public class UserInterface
    {
        public void PrintLn(Object obj)
        {
            Console.WriteLine(obj);
        }

        public void PrintTitle(string title)
        {
            Console.WriteLine("\n --" + title + " --");
        }

        public void PrintOption(char option, string description)
        {
            Console.WriteLine("(" + option + ")" + " " + description);
        }

        public char Choice(string options)
        {
            string line;
            do
            {
                Console.WriteLine("Choice [" + options + "]: ");
                line = Console.ReadLine();
            } while (!(line.Length == 1 && options.Contains(line)));

            return line[0];
        }

        public string ReadString(string prompt, string defaultValue)
        {
            PrintPrompt(prompt, defaultValue);
            string line = Console.ReadLine();
            return string.IsNullOrEmpty(line) ? defaultValue : line;
        }

        public DateTime ReadDate(string prompt, DateTime defaultValue)
        {
            while (true)
            {
                PrintPrompt(prompt, defaultValue.Date.ToString("MM/dd/yyyy"));
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    return defaultValue;
                }

                try
                {
                    return Convert.ToDateTime(line);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Bad date format! Specify in the following way: dd/mm/yyyy");
                }
            }
        }
        
        public int ReadInt(string prompt, int defaultValue)
        {
            while (true)
            {
                PrintPrompt(prompt, defaultValue);
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    return defaultValue;
                }

                try
                {
                    return int.Parse(line);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Enter an integer!");
                }
            }
        }

        private void PrintPrompt(string prompt, object defaultValue)
        {
            Console.WriteLine(prompt + " [" + defaultValue + "]: ");
        }
    }
}
