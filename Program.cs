﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JSONFileHandling
{
    class Program
    {
        static string path = "data.json";

        static void Main()
        {
            CreateJSON();

            Console.WriteLine("\nOriginal JSON File:");
            ReadJSON();

            ModifyJSON();

            Console.WriteLine("\nUpdated JSON File:");
            ReadJSON();

            Console.WriteLine("\nSorted JSON Data:");
            ReadJSONSorted();

            Console.ReadLine();
        }

        static void CreateJSON()
        {
            var employees = new List<Employee>
            {
                new Employee { ID = 101, Name = "Aniket", Age = 23, Department = "IT" },
                new Employee { ID = 102, Name = "Aditya", Age = 24, Department = "HR" },
                new Employee { ID = 103, Name = "Aman", Age = 22, Department = "Technical" },
                new Employee { ID = 104, Name = "Anirudh", Age = 25, Department = "IT" }
            };

            string jsonString = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(jsonString);
            }

            Console.WriteLine("JSON File Created Successfully!");
        }

        static void ReadJSON()
        {
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string jsonString = reader.ReadToEnd();
                    Console.WriteLine(jsonString);
                }
            }
            else
            {
                Console.WriteLine("JSON File Not Found!");
            }
        }

        static void ModifyJSON()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("JSON File Not Found!");
                return;
            }

            string jsonString;
            using (StreamReader reader = new StreamReader(path))
            {
                jsonString = reader.ReadToEnd();
            }

            var employees = JsonSerializer.Deserialize<List<Employee>>(jsonString);

            foreach (var emp in employees)
            {
                if (emp.ID == 102)
                {
                    emp.Age = 29;
                    break;
                }
            }

            jsonString = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(jsonString);
            }

            Console.WriteLine("JSON File Updated Successfully!");
        }

        static void ReadJSONSorted()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("JSON File Not Found!");
                return;
            }

            string jsonString;
            using (StreamReader reader = new StreamReader(path))
            {
                jsonString = reader.ReadToEnd();
            }

            var employees = JsonSerializer.Deserialize<List<Employee>>(jsonString);

            for (int i = 0; i < employees.Count - 1; i++)
            {
                for (int j = i + 1; j < employees.Count; j++)
                {
                    if (employees[i].Age > employees[j].Age ||
                        (employees[i].Age == employees[j].Age && string.Compare(employees[i].Name, employees[j].Name) < 0))
                    {
                        Employee temp = employees[i];
                        employees[i] = employees[j];
                        employees[j] = temp;
                    }
                }
            }

            string sortedJson = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(sortedJson);

        }
    }

    class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
    }
}
