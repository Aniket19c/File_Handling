using System;
using System.Collections.Generic;
using System.IO;

namespace CSVFileHandling
{
    class Program
    {
        static string path = "Data.csv";

        static void Main()
        {
            CreateCSV();

            Console.WriteLine("\nOriginal CSV File:");
            ReadCSV();

            ModifyCSV();

            Console.WriteLine("\nUpdated CSV File:");
            ReadCSV();
        }

        static void CreateCSV()
        {
            string[] csvData =
            {
                "ID,Name,Age,Department",
                "101,Aniket,23,IT",
                "102,Aditya,24,HR",
                "103,Aman,22,Technical",
                "104,Anirudh,25,IT"
            };

            File.WriteAllLines(path, csvData);
            Console.WriteLine("CSV File Created Successfully!");
        }

        static void ReadCSV()
        {
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("CSV File Not Found!");
            }
        }

        static void ModifyCSV()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("CSV File Not Found!");
                return;
            }

            List<string> lines = new List<string>(File.ReadAllLines(path));

            for (int i = 1; i < lines.Count; i++)
            {
                string[] columns = lines[i].Split(',');

                if (columns[0] == "102")  
                {
                    columns[2] = "29";  
                    lines[i] = string.Join(",", columns);
                    break; 
                }
            }

            File.WriteAllLines(path, lines);
            Console.WriteLine("CSV File Updated Successfully!");
        }
    }
}
