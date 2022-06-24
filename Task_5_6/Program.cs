using System;

namespace Task_5_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userData = GetUserData();
            ShowUserData(userData);
            Console.ReadKey();
        }

        private static bool CheckValue(ValueType valueType, object value)
        {
            switch (valueType)
            {
                case ValueType.Int:
                    Int32.TryParse(value.ToString(), out int val);
                    if (val > 0) 
                        return false;
                    break;
                case ValueType.String:
                    if (!string.IsNullOrEmpty(value.ToString()) && !Int32.TryParse(value.ToString(), out int _))
                        return false;
                    break;
                default:
                    throw new NotImplementedException();
            }
            return true;
        }

        private static string[] GetArray(int qnt, ArrayType arrayType)
        {
            string[] array = new string[qnt];
            string arrayDescr = arrayType == ArrayType.Pet ? "кличку питомца" : arrayType == ArrayType.Color ? "цвет" : "";
            for (int i = 0; i < qnt; i++)
            {
                string item;
                do
                {
                    Console.Write($"Укажите {arrayDescr} {i + 1}: "); ;
                    item = Console.ReadLine();
                }
                while (CheckValue(ValueType.String, item));
                array[i] = item;
            }
            return array;
        }

        private static void ShowUserData((string name, string surname, int age, string[] pets, string[] colors) userData)
        {
            Console.WriteLine();
            Console.WriteLine($"Имя пользователя: {userData.name}");
            Console.WriteLine($"Фамилия пользователя: {userData.surname}");
            Console.WriteLine($"Возраст пользователя: {userData.age.ToString()}");
            if (userData.pets != null)
                Console.WriteLine($"Питомцы пользователя: {string.Join(", ", userData.pets)}");
            Console.WriteLine($"Любимые цвета пользователя: {string.Join(", ", userData.colors)}");
        }

        private static (string name, string surname, int age, string[] pets, string[] colors) GetUserData()
        {
            (string name, string surname, int age, string[] pets, string[] colors) userData = new ("", "", 0, null, null);
            // Имя
            do
            {
                Console.Write("Введите имя: ");
                userData.name = Console.ReadLine();
            }
            while(CheckValue(ValueType.String, userData.name));
            // Фамилия
            do
            {
                Console.Write("Введите фамилию: ");
                userData.surname = Console.ReadLine();
            }
            while (CheckValue(ValueType.String, userData.surname));
            // Возраст
            int age;
            do
            {
                Console.Write("Введите возраст: ");
                Int32.TryParse(Console.ReadLine(), out age);
            }
            while (CheckValue(ValueType.Int, age));
            userData.age = age;
            // Питомцы
            Console.Write("У вас есть питомец (да/нет): ");
            if (Console.ReadLine().ToLower() == "да")
            {
                int petsQnt = 0;
                do
                {
                    Console.Write("Укажите количество питомцев: ");
                    Int32.TryParse(Console.ReadLine(), out int qnt);
                    petsQnt = qnt;
                }
                while (CheckValue(ValueType.Int, petsQnt));
                userData.pets = GetArray(petsQnt, ArrayType.Pet);
            }
            // Цвета
            int colorsQnt = 0;
            do
            {
                Console.Write("Укажите количество любимых цветов: ");
                Int32.TryParse(Console.ReadLine(), out int qnt);
                colorsQnt = qnt;
            }
            while (CheckValue(ValueType.Int, colorsQnt));
            userData.colors = GetArray(colorsQnt, ArrayType.Color);

            return userData;
        }
    }
}
