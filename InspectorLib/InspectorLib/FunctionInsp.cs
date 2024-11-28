using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;

namespace InspectorLib
{
    public class FunctionInsp
    {
        public string NameIns { get; set; } = "Автоинспекция г. Чита"; //Название автоинспекции
        public string MainInspector { get; set; } = "Васильев Василий Иванович";//ФИО главного инспектора
        public string[] Inspectors { get; set; } =
        {
            "Иванов И.И.",
            "Зиронов Т.А.",
            "Миронов А.В.",
            "Васильев В.И."
        };//Список всех инспекторов
        public void GetInspector()// Метод вывода всех инспекторов
        {
            Console.WriteLine(MainInspector);
        }
        public void GetCarInspection() //Метод вывод названия инспекции
        {
            Console.WriteLine(NameIns);
        }
        public void SetInspector(string fullname)//метод изменения главного инспектора
        {
            Console.WriteLine("Введите ФИО инспектора");
            fullname = Console.ReadLine();
            if (Inspectors.Contains(fullname))//Проверяем есть ли такой инспектор в списке в формате Иванов И.И.
            {
                MainInspector = fullname;
                Console.WriteLine("ФИО главного инспектора изменено");
            }
            else//если не нашли инспектора, пробуем форматировать запись, вдруг пользователь написал Иванов Иван Иванович
            {
                var parts = fullname.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);//делим строку на элементы
                if (parts.Length == 3)//если элемента у нас 3, то есть имя, фамилия, отчество
                {
                    string lastName = parts[0];
                    string firstName = parts[1].Substring(0, 1) + ".";
                    string patronymic = parts[2].Substring(0, 1) + ".";
                    fullname = lastName + " " + firstName + patronymic;
                    if (Inspectors.Contains(fullname))//проверяем есть ли инспектор в измененном формате в списке
                    {
                        MainInspector = fullname;
                        Console.WriteLine("ФИО главного инспектора изменено");
                    }
                    else { Console.WriteLine("Изменить ФИО главного инспектора не удалось"); }
                }
                else Console.WriteLine("Изменить ФИО главного инспектора не удалось");
            }
        }
        public void GenerateNumber(string number, char symbol, string code)//генерируем номер
        {
            Random random = new Random();
            number = Convert.ToString(random.Next(100, 999));//генерируем номер от 100 до 999
            symbol = Convert.ToChar(random.Next('А', 'Я' + 1));//генерируем символ от А до Я
            symbol = char.ToUpper(symbol);
            code = "75";
            Console.WriteLine(Convert.ToString($"Ваш новый госномер: {symbol}{number}_{code}"));//Соединяем номер,символ и код, и выводим 

        }
        public void GetWorker()
        {
            for (int i = 0; i < Inspectors.Length; i++)//перебираем элементы массива и затем выводим
            {
                Console.WriteLine(Inspectors[i]);
            }
        }
        public void AddWorker(string newWorker)
        {
            bool exit = false;
            bool correctFormat = false;
            while (exit == false)//пока переменная false, цикл продолжается
            {
                Console.WriteLine("Введите ФИО нового сотрудника: ");
                newWorker = Console.ReadLine();//задаем переменной значение
                if (newWorker != null)//проверка что строка не пуста
                {
                    if (Inspectors.Contains(newWorker))//есть ли этот инспектор в списке
                    {
                        Console.WriteLine("Найдено совпадение, добавить сотрудника? Да/Нет");
                        string flag = Console.ReadLine();
                        if (flag == "Да")
                        {
                            string[] newInspectors = new string[Inspectors.Length + 1];
                            // Копируем старый массив в новый
                            for (int i = 0; i < Inspectors.Length; i++)
                            {
                                newInspectors[i] = Inspectors[i];
                            }
                            // Добавляем нового инспектора
                            newInspectors[newInspectors.Length - 1] = newWorker;
                            // Устанавливаем новый массив как текущий массив инспекторов
                            Inspectors = newInspectors;
                            Console.WriteLine("Новый сотрудник,успешно добавлен");
                            exit = true;
                        }
                        else
                        {
                            Console.WriteLine("Хотите, добавить другого сотрудника? Да/Нет");
                            flag = Console.ReadLine();
                            if (flag == "Да")
                            {
                                newWorker = String.Empty;
                            }
                            else
                            {
                                Console.WriteLine("Добавление сотрудника отменено");
                                exit = true;
                            }
                        }
                    }
                    else//если совпадений не найдено пробуем форматировать нашу строку и проверяем есть ли такой инспектор
                    {
                        if (correctFormat == false)
                        {
                            string format = newWorker;
                            var parts = format.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length == 3)
                            {
                                string lastName = parts[0];
                                string firstName = parts[1].Substring(0, 1) + ".";
                                string patronymic = parts[2].Substring(0, 1) + ".";
                                format = lastName + " " + firstName + patronymic;
                                if (Inspectors.Contains(format))
                                {
                                    Console.WriteLine("Найдено совпадение, добавить сотрудника? Да/Нет");
                                    string flag = Console.ReadLine();
                                    if (flag == "Да")
                                    {
                                        string[] newInspectors = new string[Inspectors.Length + 1];
                                        // Копируем старый массив в новый
                                        for (int i = 0; i < Inspectors.Length; i++)
                                        {
                                            newInspectors[i] = Inspectors[i];
                                        }
                                        // Добавляем нового инспектора
                                        newInspectors[newInspectors.Length - 1] = format;
                                        // Устанавливаем новый массив как текущий массив инспекторов
                                        Inspectors = newInspectors;
                                        exit = true;
                                        Console.WriteLine("Новый сотрудник,успешно добавлен");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Хотите, добавить другого сотрудника? Да/Нет");
                                        flag = Console.ReadLine();
                                        if (flag == "Да")
                                        {
                                            format = String.Empty;
                                            newWorker = String.Empty;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Добавление сотрудника отменено");
                                            exit = true;
                                        }
                                    }
                                }
                                else//если сотрудника нет, добавляем
                                {
                                    string[] newInspectors = new string[Inspectors.Length + 1];
                                    // Копируем старый массив в новый
                                    for (int i = 0; i < Inspectors.Length; i++)
                                    {
                                        newInspectors[i] = Inspectors[i];
                                    }
                                    // Добавляем нового инспектора
                                    newInspectors[newInspectors.Length - 1] = newWorker;
                                    // Устанавливаем новый массив как текущий массив инспекторов
                                    Inspectors = newInspectors;
                                    Console.WriteLine("Новый сотрудник,успешно добавлен");
                                    exit = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Неверный формат ФИО");
                            }
                        }



                    }
                }
            }

        }
    }
}
    

