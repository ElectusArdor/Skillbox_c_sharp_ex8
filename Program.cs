using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Ex8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FirstTask();
            NextTask();

            SecondTask();
            NextTask();

            ThirdTask();
            NextTask();

            FourthTask();
            NextTask();
        }

        /// <summary>
        /// Переход между заданиями.
        /// </summary>
        static void NextTask()
        {
            Console.WriteLine("Для продолжения нажмите любую клавишу.\n\n");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Решение на задание №1
        /// </summary>
        static void FirstTask()
        {
            List<int> list = new List<int>();

            FillList(list);
            PrintList(list);
            RemoveElements(ref list);
            PrintList(list);

            void FillList(List<int> lst)
            {
                Random rnd = new Random();
                for (int i = 0; i < 100; i++) lst.Add(rnd.Next(0, 101));
            }

            void PrintList(List<int> lst)
            {
                foreach (int i in lst)
                    Console.Write($"{i} ");
                Console.WriteLine("\n");
            }

            void RemoveElements(ref List<int> lst)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i] > 25 & lst[i] < 50)
                    {
                        lst.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        /// <summary>
        /// Решение на задание №2
        /// </summary>
        static void SecondTask()
        {
            Dictionary<string, string> phoneBook = new Dictionary<string, string>();

            FillPhoneBook();
            UsePhoneBook();

            void FillPhoneBook()
            {
                Console.WriteLine("\nВведите номера в телефонную книгу.\nДля завершения ввода номеров введите пустую строку.");
                while (true)
                {
                    Console.Write("\nНомер: ");
                    string phoneNumber = Console.ReadLine();
                    if (phoneNumber == "") break;

                    Console.Write("\nФИО: ");
                    string fio = Console.ReadLine();
                    if (fio == "") break;

                    phoneBook.Add(phoneNumber, fio);
                }
            }

            void UsePhoneBook()
            {
                Console.WriteLine("\nВведите номер, чтобы узнат, кому он принадлежит.");
                while (true)
                {
                    Console.Write("\nНомер: ");
                    string phoneNumber = Console.ReadLine();
                    if (phoneNumber == "") break;

                    if (phoneBook.TryGetValue(phoneNumber, out string value))
                        Console.WriteLine(value);
                    else Console.WriteLine("Нет такого номера.");
                }
            }
        }
        
        /// <summary>
        /// Решение на задание №3
        /// </summary>
        static void ThirdTask()
        {
            HashSet<int> intHash = new HashSet<int>();

            while (true)
            {
                Console.WriteLine("Введите целое число");
                string data = Console.ReadLine();

                if (data == "") break;
                else if (!int.TryParse(data, out int num))
                {
                    Console.WriteLine("Это не целое число");
                    continue;
                }
                else if (intHash.Contains(num))
                {
                    Console.WriteLine("Такое число уже есть.");
                    continue;
                }
                else
                {
                    intHash.Add(num);
                    Console.WriteLine("Число доавлено.");
                }
            }
        }

        /// <summary>
        /// Решение на задание №4
        /// </summary>
        static void FourthTask()
        {
            SerializeNoteBook();
            NextTask();
            ShowNoteBook();
            Console.ReadKey();

            /// <summary>
            /// Заполнение записной книжки.
            /// </summary>
            void SerializeNoteBook()
            {
                XElement noteBook = new XElement("NoteBook");

                while (true)
                {
                    XElement xEl = new XElement("Person");

                    Console.Write("Введите данные в записную книжку.\nФИО контакта: ");
                    XAttribute nameAtrb = new XAttribute("name", Console.ReadLine());
                    xEl.Add(nameAtrb);

                    Console.Write("\nУлица: ");
                    XElement street = new XElement("Street", Console.ReadLine());
                    Console.Write("\nДом: ");
                    XElement houseNumber = new XElement("HouseNumber", Console.ReadLine());
                    Console.Write("\nКвартира: ");
                    XElement flatNumber = new XElement("FlatNumber", Console.ReadLine());

                    XElement address = new XElement("Address");
                    address.Add(street);
                    address.Add(houseNumber);
                    address.Add(flatNumber);

                    Console.Write("\nМобильный телефон: ");
                    XElement mobilePhone = new XElement("mobilePhone", Console.ReadLine());
                    Console.Write("\nДомашний телефон: ");
                    XElement flatPhone = new XElement("flatPhone", Console.ReadLine());

                    XElement phones = new XElement("Phones");
                    phones.Add(mobilePhone);
                    phones.Add(flatPhone);

                    xEl.Add(address);
                    xEl.Add(phones);

                    noteBook.Add(xEl);

                    Console.WriteLine("Готово. Сделать ещё запись? y/n");
                    string answer = Console.ReadLine();

                    if (answer != "Y" & answer != "y")
                        break;
                }

                noteBook.Save("NoteBook.xml");
            }

            /// <summary>
            /// отображение записной книжки.
            /// </summary>
            void ShowNoteBook()
            {
                string xml = File.ReadAllText("NoteBook.xml");

                var personsColl = XDocument.Parse(xml).Descendants("Person").ToList();

                foreach (var person in personsColl)
                {
                    Console.WriteLine("Имя: {0}\nАдрес: ул. {1} дом {2} квартира {3}\nМобильный телефон: {4}\nДомашний телефон: {5}\n\n",
                        person.Attribute("name").Value,
                        person.Element("Address").Element("Street").Value,
                        person.Element("Address").Element("HouseNumber").Value,
                        person.Element("Address").Element("FlatNumber").Value,
                        person.Element("Phones").Element("mobilePhone").Value,
                        person.Element("Phones").Element("flatPhone").Value
                        );
                }
            }
        }
    }
}
