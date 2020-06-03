using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using EKRLib;
using System.Runtime.Serialization.Json;

namespace EKRApp1
{
    internal class Program
    {
        private const string path = "boxes.json";
        public static Random rnd = new Random();
        
        public static void Main(string[] args)
        {
            do
            {
                int N = ReadInt("Введите N: ", 1);
                
                Collection<Box> boxes = GetCollection(N);
                PrintCollection(boxes);
                SerializeCollection(boxes);

                Console.WriteLine("Для повторного запуска нажмите Enter...");
            } while (Console.ReadKey(true).Key == ConsoleKey.Enter);
        }

        public static int ReadInt(string message, int left = int.MinValue, int right = int.MaxValue)
        {
            int res;
            do
            {
                Console.Write(message);
            } while (!int.TryParse(Console.ReadLine(), out res) || res < left || res > right);

            return res;
        }

        public static double RandomDouble(double left = -3, double right = 10) =>
            left + rnd.NextDouble() * (right - left);
        
        private static void SerializeCollection(Collection<Box> boxes)
        {
            DataContractJsonSerializer ser =
                new DataContractJsonSerializer(typeof(Collection<Box>), new Type[] {typeof(Box)});

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    ser.WriteObject(fs, boxes);
                }
            }
            catch (IOException)
            {
                Console.WriteLine($"Возникла ошибка при работе с файлом {path}");
            }
            catch (SerializationException)
            {
                Console.WriteLine("Возникла ошибка при сериализации");
            }
            catch (Exception)
            {
                Console.WriteLine("Возникла непредвиденная ошибка");
            }
        }

        private static void PrintCollection(IEnumerable<Box> boxes)
        {
            foreach (var box in boxes)
            {
                Console.WriteLine(box);
            }
        }

        private static Collection<Box> GetCollection(int N)
        {
            Collection<Box> boxes = new Collection<Box>();

            for (int i = 0; i < N; i++)
            {
                try
                {
                    Box box = new Box
                    {
                        Weight = RandomDouble(),
                        A = RandomDouble(),
                        B = RandomDouble(),
                        C = RandomDouble()
                    };
                    boxes.Add(box);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Ошибка при создании объекта Box: {e.Message}");
                    i--;
                }
            }

            return boxes;
        }
    }
}