
using System;

namespace Crub
{
    class CrubQuest
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, Bitch!");
            Console.WriteLine(NumberTime(6, 7));
            var dog = new Dog(5)
            {
                Name = "BleepBoop"
            };
            Console.WriteLine($"Dog's name: {dog.Name}, Age: {dog.Age}");
        }

        static int NumberTime(int num1, int num2)
        {
            return num1 + num2;
        }
    }
}