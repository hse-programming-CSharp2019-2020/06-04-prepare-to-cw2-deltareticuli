using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using EKRLib;

namespace EKRApp2
{
    internal class Program
    {
        private const string path = "../../../EKRApp1/bin/Debug/boxes.json";

        public static void Main(string[] args)
        {
            do
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"Файла {path} не существует. Возможно, первая программа не была запущена.");
                    return;
                }

                Collection<Box> boxes = DeserializeCollection() ?? new Collection<Box>();
                PrintCollection(boxes);

                Console.WriteLine($"{Environment.NewLine}Первый LINQ-запрос: ");
                PrintCollection(boxes.Where(x => x.GetLongestSideSize() > 3)
                    .OrderByDescending(x => x.GetLongestSideSize()));

                Console.WriteLine($"{Environment.NewLine}Второй LINQ-запрос: ");
                foreach (var collection in boxes.GroupBy(x => x.Weight))
                {
                    Console.WriteLine($"Коробки с массой {collection.Key:f2}: ");
                    PrintCollection(collection);
                }

                Console.WriteLine($"{Environment.NewLine}Третий LINQ-запрос: ");

                double maxWeight = boxes.Max(x => x.Weight);
                var query = boxes.Where(x => x.Weight.Equals(maxWeight));
                PrintCollection(query);
                Console.WriteLine($"Количество: {query.Count()}");

                Console.WriteLine("Для повторного запуска нажмите Enter...");
            } while (Console.ReadKey(true).Key == ConsoleKey.Enter);
        }

        private static void PrintCollection(IEnumerable<Box> boxes)
        {
            foreach (var box in boxes)
            {
                Console.WriteLine(box);
            }
        }

        private static Collection<Box> DeserializeCollection()
        {
            DataContractJsonSerializer ser =
                new DataContractJsonSerializer(typeof(Collection<Box>), new Type[] {typeof(Box)});
            Collection<Box> res;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    res = (Collection<Box>) ser.ReadObject(fs);
                }

                return res;
            }
            catch (IOException)
            {
                Console.WriteLine($"Возникла ошибка при работе с файлом {path}");
                return null;
            }
            catch (Exception)
            {
                Console.WriteLine("Возникла непредвиденная ошибка");
                return null;
            }
        }
    }
}