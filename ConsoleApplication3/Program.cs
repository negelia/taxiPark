using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication3
{

    /* 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */

    //Класс пользователей
    class Users
    {
        public string role { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public int salary { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }

    //Класс поездок
    class Ride
    {
        public string code { get; set; }
        public string addressTo { get; set; }
        public string addressFrom { get; set; }
        public string category { get; set; }
        public int cost { get; set; }
    }

    //Класс заказов
    class Order
    {
        public int usr { get; set; }
        public List<int> lst { get; set; }
        public string date { get; set; }
        public int driver { get; set; }
        public string status { get; set; }
    }


    //Класс рандомайзера
    public class Randomizer
    {
        //Генерация кода поездки
        public string RandomizeString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

    }

    //Класс для проверок
    public class Checker
    {

        //Проверка логина
        public static bool IsUsernameValid(string username)
        {
            string pattern = "^[a-zA-Z][a-zA-Z0-9]*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(username);
        }

        //Проверка ФИО
        public static bool IsFIOValid(string input)
        {
            //string input = "Иванов Иван Иванович"; // Пример ввода ФИО только русские буквы, пробелы и дефисы
            // Паттерн для ФИО только русские буквы, пробелы и дефисы

            string pattern = @"^[А-ЯЁІЇЄҐ][а-яёіїєґ]+(\s[А-ЯЁІЇЄҐ][а-яёіїєґ]+)?(\s[А-ЯЁІЇЄҐ][а-яёіїєґ]+)?$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        //Проверка адрес
        public static bool IsAddressValid(string input)
        {
            //string input = "Москва, ул. Пушкина, д. 10";

            string pattern = @"^[А-Яа-яёЁa-zA-Z\s\.\-]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        //Проверка на наличие чисел
        public static bool IsNumberValid(string input)
        {
            string pattern = @"\d+";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
    }

    

    class TaxiPark
    {
        public bool status = false;

        //Список пользователей
        List<Users> user = new List<Users>();

        //Список заказов
        List<Order> ordr = new List<Order>();

        //Список поездок
        List<Ride> rides = new List<Ride>();

        bool f2;

        void Start()
        {
            //Проверка на наличие файла с пользователями
            if (File.Exists("users.txt"))
            {
                //Чтение файла
                using (StreamReader sr = new StreamReader("users.txt", System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Чтение строк файла
                        Users tmp = new Users();
                        tmp.role = line;
                        tmp.name = sr.ReadLine();
                        tmp.age = Convert.ToInt32(sr.ReadLine());
                        tmp.salary = Convert.ToInt32(sr.ReadLine());
                        tmp.login = sr.ReadLine();
                        tmp.password = sr.ReadLine();
                        user.Add(tmp);
                    }
                }
            }
            else
            {
                Users tmp = new Users();

                try
                {

                    do
                    {
                        Console.Write("Пользователи не обнаружены\nЗарегистрируйте администратора:\n    ФИО только русские буквы, пробелы и дефисы: ");
                        tmp.name = Console.ReadLine();
                    } while (Checker.IsFIOValid(tmp.name) == false);

                    tmp.role = "admin";

                    do
                    {
                        Console.Write(" Возраст больше 18: ");
                        tmp.age = Convert.ToInt32(Console.ReadLine());
                    } while (tmp.age < 18 || Checker.IsNumberValid(tmp.age.ToString()) == false);

                    do
                    {
                        Console.Write(" Зарплата не менее 15 000 руб.: ");
                        tmp.salary = Convert.ToInt32(Console.ReadLine());
                    } while (tmp.salary < 15000 || Checker.IsNumberValid(tmp.salary.ToString()) == false);


                    do
                    {
                        Console.Write(" Логин: ");
                        tmp.login = Console.ReadLine();
                    } while (Checker.IsUsernameValid(tmp.login) == false);


                    do
                    {
                        Console.Write(" Пароль: ");
                        tmp.password = Console.ReadLine();
                    } while (Checker.IsUsernameValid(tmp.password) == false);

                    user.Add(tmp);

                }
                catch (Exception e) { }



            }
            if (File.Exists("rides.txt"))
            {
                using (StreamReader sr2 = new StreamReader("rides.txt", System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr2.ReadLine()) != null)
                    {
                        Ride ride = new Ride();
                        ride.code = line;
                        ride.addressTo = sr2.ReadLine();
                        ride.addressFrom = sr2.ReadLine();
                        ride.category = sr2.ReadLine();
                        ride.cost = Convert.ToInt32(sr2.ReadLine());

                        rides.Add(ride);
                    }
                }
            }
            if (File.Exists("order.txt"))
            {
                using (StreamReader sr3 = new StreamReader("order.txt", System.Text.Encoding.Default))
                {
                    string line;
                    int n;
                    while ((line = sr3.ReadLine()) != null)
                    {
                        Order tmp = new Order();
                        tmp.usr = Convert.ToInt32(line);
                        n = Convert.ToInt32(sr3.ReadLine());
                        tmp.lst = new List<int>();
                        for (int j = 0; j < n; j++)
                        {
                            tmp.lst.Add(Convert.ToInt32(sr3.ReadLine()));
                        }

                        tmp.date = sr3.ReadLine();
                        tmp.driver = Convert.ToInt32(sr3.ReadLine());
                        tmp.status = sr3.ReadLine();

                        ordr.Add(tmp);
                    }
                }
            }
        }

        void SaveUsers()
        {
            using (StreamWriter sw = new StreamWriter("users.txt", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < user.Count; i++)
                {
                    sw.WriteLine(user[i].role);
                    sw.WriteLine(user[i].name);
                    sw.WriteLine(user[i].age);
                    sw.WriteLine(user[i].salary);
                    sw.WriteLine(user[i].login);
                    sw.WriteLine(user[i].password);
                }
            }
            using (StreamWriter sw2 = new StreamWriter("rides.txt", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < rides.Count; i++)
                {
                    sw2.WriteLine(rides[i].code);
                    sw2.WriteLine(rides[i].addressTo);
                    sw2.WriteLine(rides[i].addressFrom);
                    sw2.WriteLine(rides[i].category);
                    sw2.WriteLine(rides[i].cost);

                }
            }
            using (StreamWriter sw3 = new StreamWriter("order.txt", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < ordr.Count; i++)
                {
                    sw3.WriteLine(ordr[i].usr);
                    sw3.WriteLine(ordr[i].lst.Count);
                    for (int j = 0; j < ordr[i].lst.Count; j++)
                    {
                        sw3.WriteLine(ordr[i].lst[j]);
                    }
                    sw3.WriteLine(ordr[i].date);
                    sw3.WriteLine(ordr[i].driver);
                    sw3.WriteLine(ordr[i].status);
                }
            }
        }

        //Поиск роли пользователя в списке
        string FindRole(string login)
        {
            for (int i = 0; i < user.Count; i++)
            {
                if (login == user[i].login) return user[i].role;
            }
            return "";
        }

        //Поиск пароля в списке
        string GetPass(string login)
        {
            for (int i = 0; i < user.Count; i++)
                if (login == user[i].login) return user[i].password;
            return "";
        }

        //Поиск id пользователя в списке
        int GetInd(string login)
        {
            for (int i = 0; i < user.Count; i++)
                if (login == user[i].login) return i;
            return -1;
        }

        public void StartProg()
        {
            Start();
            MainMenu();
            SaveUsers();
        }

        void AdminMenu()
        {

            //Администратор: добавление, редактирование и удаление пользователей (ФИО, Логин, Пароль)(кроме покупателей). Просмотр ВСЕХ данных
            int t = 0;
            string login = "";
            bool fl;
            do
            {
                Console.Clear();

                Console.Write("1. Добавление\n2. Редактирование\n3. Удаление\n4. Просмотр всех пользователей\n0. Выход\n\n>>");
                t = Convert.ToInt32(Console.ReadLine());

                switch (t)
                {
                    case 1:

                        Users tmp = new Users();

                        try
                        {

                            do
                            {
                                Console.Write(" ФИО только русские буквы, пробелы и дефисы: ");
                                tmp.name = Console.ReadLine();
                            } while (Checker.IsFIOValid(tmp.name) == false);


                            do
                            {
                                Console.Write("Роль: \n 1. Администратор\n 2. Кадровый сотрудник\n 3. Диспетчер\n 4. Водитель\n 5. Бухгалтер\n\n>>");
                                t = Convert.ToInt32(Console.ReadLine());
                            } while (t < 1 || t > 5);


                            switch (t)
                            {
                                case 1: tmp.role = "admin"; break;
                                case 2: tmp.role = "hr-manager"; break;
                                case 3: tmp.role = "dispatcher"; break;
                                case 4: tmp.role = "driver"; break;
                                case 5: tmp.role = "bookkeeping"; break;
                            }

                            do
                            {
                                Console.Write(" Возраст больше 18: ");
                                tmp.age = Convert.ToInt32(Console.ReadLine());
                            } while (tmp.age < 18 || Checker.IsNumberValid(tmp.age.ToString()) == false);

                            do
                            {
                                Console.Write(" Зарплата не менее 15 000 руб.: ");
                                tmp.salary = Convert.ToInt32(Console.ReadLine());
                            } while (tmp.salary < 15000 || Checker.IsNumberValid(tmp.salary.ToString()) == false);

                            do
                            {
                                Console.Write(" Логин: ");
                                tmp.login = Console.ReadLine();
                            } while (GetPass(tmp.login) != "" || Checker.IsUsernameValid(tmp.login) == false);

                            do
                            {
                                Console.Write(" Пароль: ");
                                tmp.password = Console.ReadLine();
                            } while (Checker.IsUsernameValid(tmp.password) == false);

                            user.Add(tmp);

                        }
                        catch (Exception ex) { }


                        break;

                    case 2:
                        fl = false;

                        try
                        {

                            do
                            {
                                Console.Write("Логин: ");
                                login = Convert.ToString(Console.ReadLine());
                            } while (Checker.IsUsernameValid(login) == false);

                            for (int i = 0; i < user.Count; i++)
                            {
                                if (login == user[i].login)
                                {
                                    do
                                    {
                                        Console.Write(" Возраст больше 18: ");
                                        user[i].age = Convert.ToInt32(Console.ReadLine());
                                    } while (user[i].age < 18 || Checker.IsNumberValid(user[i].age.ToString()) == false);

                                    do
                                    {
                                        Console.Write(" Зарплата не менее 15 000 руб.: ");
                                        user[i].salary = Convert.ToInt32(Console.ReadLine());
                                    } while (user[i].salary < 15000 || Checker.IsNumberValid(user[i].salary.ToString()) == false);

                                    fl = true;
                                }
                            }
                            if (!fl) Console.WriteLine("Не найден такой пользователь ");

                        }
                        catch (Exception ex) { }

                        break;

                    case 3:
                        fl = false;

                        try
                        {

                            do
                            {
                                Console.Write("Логин: ");
                                login = Convert.ToString(Console.ReadLine());
                            } while (Checker.IsUsernameValid(login) == false);

                            for (int i = 0; i < user.Count; i++)
                            {
                                if (login == user[i].login)
                                {
                                    user.RemoveAt(i);
                                    fl = true;
                                    break;
                                }
                            }
                            if (!fl) Console.WriteLine("Не найден такой пользователь ");

                        }
                        catch (Exception ex) { }



                        break;
                    case 4:
                        if (user.Count > 0)
                        {
                            Console.WriteLine("Логин\tПароль\tДолжность\tЗарплата\tВозраст\tФИО");
                            for (int i = 0; i < user.Count; i++)
                            {
                                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", user[i].login, user[i].password, user[i].role, user[i].salary, user[i].age, user[i].name);
                            }
                        }

                        break;
                }
                Console.ReadKey();
            } while (t != 0);
        }

        void HRMenu()
        {

            //Кадры: просмотр всех данных сотрудников (кроме пароля), редактирования данных, увольнение сотрудников.
            int t = 0;
            string login = "";
            bool fl;
            do
            {


                Console.Clear();

                do
                {
                    Console.Write("1. Редактирование\n2. Увольнение\n3. Просмотр сотрудников\n0. Выход\n\n>>");
                    t = Convert.ToInt32(Console.ReadLine());

                } while (t < 0 || t > 3);

                switch (t)
                {

                    case 1:
                        fl = false;

                        try
                        {

                            do
                            {
                                Console.Write("Логин: ");
                                login = Convert.ToString(Console.ReadLine());
                            } while (Checker.IsUsernameValid(login) == false);

                            for (int i = 0; i < user.Count; i++)
                            {
                                if (login == user[i].login)
                                {
                                    do
                                    {
                                        Console.Write("Роль: \n 1. Администратор\n 2. Кадровый сотрудник\n 3. Диспетчер\n 4. Водитель\n 5. Бухгалтер\n\n>>");
                                        t = Convert.ToInt32(Console.ReadLine());
                                    } while (t < 1 || t > 5);

                                    switch (t)
                                    {
                                        case 1: user[i].role = "admin"; break;
                                        case 2: user[i].role = "hr-manager"; break;
                                        case 3: user[i].role = "dispatcher"; break;
                                        case 4: user[i].role = "driver"; break;
                                        case 5: user[i].role = "bookkeeping"; break;
                                    }

                                    do
                                    {
                                        Console.Write(" Возраст больше 18: ");
                                        user[i].age = Convert.ToInt32(Console.ReadLine());
                                    } while (user[i].age < 18 || Checker.IsNumberValid(user[i].age.ToString()) == false);


                                    do
                                    {
                                        Console.Write(" Зарплата не менее 15 000 руб.: ");
                                        user[i].salary = Convert.ToInt32(Console.ReadLine());
                                    } while (user[i].salary < 15000 || Checker.IsNumberValid(user[i].salary.ToString()) == false);

                                    fl = true;

                                }
                            }
                            if (!fl) Console.WriteLine("Не найден такой пользователь ");

                        }
                        catch (Exception ex) { }

                        break;

                    case 2:
                        fl = false;

                        try
                        {

                            do
                            {
                                Console.Write("Логин: ");
                                login = Convert.ToString(Console.ReadLine());
                            } while (Checker.IsUsernameValid(login) == false);

                            for (int i = 0; i < user.Count; i++)
                            {
                                if (login == user[i].login)
                                {
                                    user.RemoveAt(i);
                                    fl = true;
                                    break;
                                }
                            }
                            if (!fl) Console.WriteLine("Не найден такой пользователь ");

                        }
                        catch (Exception ex) { }


                        break;
                    case 3:
                        if (user.Count > 0)
                        {
                            Console.WriteLine("\tЛогин\tДолжность\tЗарплата\tВозраст\tФИО");
                            for (int i = 0; i < user.Count; i++)
                            {
                                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", user[i].login, user[i].role, user[i].salary, user[i].age, user[i].name);
                            }
                        }

                        break;
                }
                Console.ReadKey();
            } while (t != 0);


        }


        void AddRide(string line)
        {

            //рандомайзер кода поездки
            Randomizer rand = new Randomizer();
            int length = 4; // длина случайной строки
            string randomString = rand.RandomizeString(length);
            line = randomString;

            Console.WriteLine("Код поездки: " + line);

            Ride ride = new Ride();

            ride.code = line;

            int cost = 200;

            try
            {
                do
                {
                    Console.Write("Адрес туда: ");
                    ride.addressTo = Console.ReadLine();
                } while (Checker.IsAddressValid(ride.addressTo) == false);

                do
                {
                    Console.Write("Адрес обратно: ");
                    ride.addressFrom = Console.ReadLine();
                } while (Checker.IsAddressValid(ride.addressFrom) == false);

                int km = 0;
                Random random = new Random();
                km = random.Next(10, 100);

                Console.WriteLine("Расстояние: " + km + " км");

                if (km > 10 && km < 30)
                {
                    cost += 200;
                }
                else if (km > 30 && km < 60)
                {
                    cost += 1000;
                }
                else if (km > 60)
                {
                    cost += 2000;
                }

                int ch = 0;
                do
                {
                    Console.Write("Категория: 1.Эконом, 2.Комфорт, 3.Бизнес ");
                    ch = Convert.ToInt32(Console.ReadLine());
                } while (ch < 1 || ch > 3);


                switch (ch)
                {
                    case 1: ride.category = "Эконом"; cost += 100; break;
                    case 2: ride.category = "Комфорт"; cost += 500; break;
                    case 3: ride.category = "Бизнес"; cost += 800; break;
                    default: ride.category = "Эконом"; break;
                }

                ride.cost = cost;


                rides.Add(ride);

            }
            catch (Exception ex) { }


        }

        void ShowOrder()
        {
            if (ordr.Count > 0)
            {
                Console.WriteLine("\tСтатус");
                for (int i = 0; i < ordr.Count; i++)
                {
                    Console.WriteLine("{0})\t{1}", i, ordr[i].status);
                }

            }
            else Console.WriteLine("Нет товаров");
        }

        void DispatchMenu()
        {
            // Диспетчер: оформление поездки, просмотр поездок.


            int t = 0;
            string str = "";
            bool fl;

            try
            {
                do
                {
                    Console.Clear();
                    Console.Write("1. Оформление поездок\n2. Просмотр поездок\n0. Выход\n\n>>");
                    t = Convert.ToInt32(Console.ReadLine());
                    switch (t)
                    {

                        case 1:
                            AddRide(str);
                            break;

                        case 2:
                            if (rides.Count > 0)
                            {
                                Console.WriteLine("\tКод\tАдрес туда\tАдрес обратно\tКатегория\tЦена");
                                for (int i = 0; i < rides.Count; i++)
                                {
                                    Console.WriteLine("{0})\t{1}\t{2}\t{3}\t{4}\t{5}", i, rides[i].code, rides[i].addressTo, rides[i].addressFrom, rides[i].category, rides[i].cost);
                                }

                            }
                            else Console.WriteLine("Нет поездок");
                            break;
                    }
                    Console.ReadKey();
                } while (t < 0 && t > 2);
            }
            catch (Exception ex) { }

        }

        void DriverMenu()
        {
            //Водитель: оформление-отмена заказа.


            int t = 0, n;
            string str;
            bool fl;
            do
            {
                Console.Clear();

                try
                {

                    do
                    {
                        Console.Write("1. Оформление-отмена заказа\n2. Просмотр поездок\n3. Просмотр заказов\n0. Выход\n\n>>");
                        t = Convert.ToInt32(Console.ReadLine());
                    } while (t < 0 || t > 4);

                    switch (t)
                    {

                        case 1:
                            if (ordr.Count > 0)
                            {
                                ShowOrder();

                                do
                                {
                                    Console.Write("Индекс заказа: ");
                                    n = Convert.ToInt32(Console.ReadLine());

                                } while (n < 0 && n >= ordr.Count);


                                Console.Write("Новый статус:\t1. В пути\t2. На месте\t3. Завершён: ");
                                int ch = Convert.ToInt32(Console.ReadLine());
                                switch (ch)
                                {
                                    case 1: ordr[n].status = "В пути"; break;
                                    case 2: ordr[n].status = "На месте"; break;
                                    case 3: ordr[n].status = "Завершён"; break;
                                }
                                Console.Write("Статус заказа изменен на: {0}\n", ordr[n].status);
                            }
                            else Console.WriteLine("Заказов нет");

                            break;

                        case 2:

                            if (rides.Count > 0)
                            {
                                Console.WriteLine("\tКод поездки\tАдрес туда\tАдрес откуда\tКатегория\tЦена");
                                for (int i = 0; i < rides.Count; i++)
                                {
                                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", i, rides[i].code, rides[i].addressTo, rides[i].addressFrom, rides[i].category, rides[i].cost);
                                }
                            }
                            else Console.WriteLine("Поездок нет");

                            break;
                        case 3:
                            if (ordr.Count > 0)
                            {
                                for (int i = 0; i < ordr.Count; i++)
                                {
                                    Console.WriteLine("{0} - {1} - {2}", user[ordr[i].usr].name, ordr[i].date, ordr[i].status);
                                    for (int j = 0; j < ordr[i].lst.Count; j++)
                                    {
                                        Console.WriteLine("\t{0} - {1}", rides[ordr[i].lst[j]]);
                                    }
                                }
                            }
                            else Console.WriteLine("Заказов нет");
                            break;
                    }
                    Console.ReadKey();
                }
                catch (Exception ex) { }

            } while (t != 0);


        }


        void BookMenu()
        {
            //Бухгалтерия: информация о доходах за покупки, общий бюджет компании.


            int t = 0, n, i;
            string str = " ";
            bool fl;
            do

            {
                Console.Clear();

                try
                {
                    do
                    {
                        Console.Write("1. Просмотр заказов\n2. Расчёт заказа\n3. Заработная плата\n0. Выход\n\n>>");
                        t = Convert.ToInt32(Console.ReadLine());
                    } while (t < 0 || t > 4);

                    switch (t)
                    {

                        case 1:
                            //Быстрый просмотр не успеваю посмотреть
                            if (ordr.Count > 0)
                            {
                                for (i = 0; i < ordr.Count; i++)
                                {
                                    Console.WriteLine("{0} - {1} - {2}", user[ordr[i].usr].name, ordr[i].date, ordr[i].status);
                                    for (int j = 0; j < ordr[i].lst.Count; j++)
                                    {
                                        Console.WriteLine("\t{0} - {1}", rides[ordr[i].lst[j]].code);
                                    }
                                }
                            }
                            else Console.WriteLine("Заказов нет");


                            break;

                        case 2:
                            if (ordr.Count > 0)
                            {
                                ShowOrder();

                                do
                                {
                                    Console.Write("Индекс заказа: ");
                                    n = Convert.ToInt32(Console.ReadLine());
                                } while (n < 0 || n >= ordr.Count);
                                Console.WriteLine("{0} - {1} - {2}", user[ordr[n].usr].name, ordr[n].date, ordr[n].status);

                                Console.WriteLine("\tКод поездки\tСтоимость");
                                for (int j = 0; j < ordr[n].lst.Count; j++)
                                {
                                    Console.WriteLine("{0} - {1}р.", rides[ordr[n].lst[j]].code, rides[ordr[n].lst[j]].cost);
                                }

                            }
                            else Console.WriteLine("Заказов нет");

                            break;
                        case 3:
                            while (Checker.IsFIOValid(str) == false)
                            {
                                Console.Write(" ФИО только русские буквы, пробелы и дефисы: ");
                                str = Console.ReadLine();
                            }


                            for (i = 0; i < user.Count; i++)
                            {
                                if (user[i].name == str)
                                {
                                    Console.WriteLine("{0} - {1} - {2}p.", user[i].name, user[i].age, user[i].salary);
                                    break;
                                }
                            }
                            if (i == 0) Console.WriteLine("Не найден такой сотрудник");
                            break;
                    }
                    Console.ReadKey();
                }
                catch (Exception ex) { }

            } while (t != 0);


        }

        void AddNewUserOrder(int log)
        {
            int n;
            string str = "";
            if (rides.Count > 0)
            {
                if (rides.Count > 0)
                {
                    Console.WriteLine("\tКод\tАдрес туда\tАдрес обратно\tКатегория\tЦена");
                    for (int i = 0; i < rides.Count; i++)
                    {
                        Console.WriteLine("{0})\t{1}\t{2}\t{3}\t{4}\t{5}", i, rides[i].code, rides[i].addressTo, rides[i].addressFrom, rides[i].category, rides[i].cost);
                    }

                }
                else Console.WriteLine("Нет поездок");

                do
                {
                    Console.WriteLine("\nНомер поездки");
                    n = Convert.ToInt32(Console.ReadLine());
                } while (n < 0 || n >= rides.Count || Checker.IsNumberValid(n.ToString()) == false);

                Order tmp = new Order();
                tmp.usr = log;
                tmp.lst = new List<int>();
                tmp.lst.Add(n);
                tmp.status = "Заказ создан";

                Console.WriteLine("Дата формирования: " + DateTime.Now);
                tmp.date = DateTime.Now.ToString();

                ordr.Add(tmp);
            }
            else {
                Console.WriteLine("Нет поездок");
                AddRide(str);
            }
                
        }

        void UserMenu(int log)
        {
            //Покупатель: добавить, удалить поездки из заказа

            int t = 0, n, i, k;
            string str;
            bool fl;

            n = 0;


        f1:
            fl = false;
            int sum = 0;
            k = 0;
            Console.Clear();
            Console.WriteLine("Вы вошли как {0} - {1}", user[log].name, user[log].age);
            for (i = 0; i < ordr.Count; i++)
            {
                if (ordr[i].usr == log)
                {
                    Console.WriteLine("{0}) {1}", k++, ordr[i].date);
                    for (int j = 0; j < ordr[i].lst.Count; j++)
                    {
                        Console.WriteLine("\t{0} - {1} - {2}р.", rides[ordr[i].lst[j]].code, ordr[i].status, rides[ordr[i].lst[j]].cost);
                    }
                }
            }
            if (k == 0)
            {
                Console.WriteLine("Заказов нет\nДобавить новый заказ? (1-да,иначе-выход)");
                n = Convert.ToInt32(Console.ReadLine());
                if (n == 1)
                {
                    AddNewUserOrder(log);
                    goto f1;
                }
            }
            else
            {

                Console.Write("1. Добавить новый заказ\n2. Редактировать заказ\n0. Выход\n\n>>");
                n = Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 1: AddNewUserOrder(log); break;
                    case 2:

                        if (rides.Count > 0) {
                            if (ordr.Count > 0) {
                                if (rides.Count > 0)
                                {
                                    Console.WriteLine("\tКод\tАдрес туда\tАдрес обратно\tКатегория\tЦена");
                                    for (k = 0; k < rides.Count; k++)
                                    {
                                        Console.WriteLine("{0})\t{1}\t{2}\t{3}\t{4}\t{5}", k, rides[k].code, rides[k].addressTo, rides[k].addressFrom, rides[k].category, rides[k].cost);
                                    }

                                }
                                else Console.WriteLine("Нет поездок");

                                try {
                                    do
                                    {
                                        Console.Write("Номер поездки: ");
                                        n = Convert.ToInt32(Console.ReadLine());
                                    } while (n < 0 || n >= k || Checker.IsNumberValid(n.ToString()) == false);
                                    k = 0;
                                    for (i = 0; i < ordr.Count; i++)
                                    {
                                        if (ordr[i].usr == log)
                                        {
                                            if (k == n)
                                            {
                                                Console.Write("1. Редактировать\n0. Выход\n\n>>");
                                                n = Convert.ToInt32(Console.ReadLine());
                                                switch (n)
                                                {
                                                    case 1:
                                                        for (int j = 0; j < ordr[i].lst.Count; j++)
                                                            Console.WriteLine("{0})\t{1} - {2} - {3}р.", j, rides[ordr[i].lst[j]].code, ordr[i].status, rides[ordr[i].lst[j]].cost);
                                                        do
                                                        {
                                                            Console.Write("Номер заказа: ");
                                                            n = Convert.ToInt32(Console.ReadLine());
                                                        } while (n < 0 || n >= ordr[i].lst.Count || Checker.IsNumberValid(n.ToString()) == false);

                                                        int ch = 0;
                                                        do {
                                                            Console.Write("Отменить поездку? \t1. Отменить\t0. Выход: ");
                                                            ch = Convert.ToInt32(Console.ReadLine());
                                                        } while (ch < 0 || ch > 1 || Checker.IsNumberValid(n.ToString()) == false);
                                                        
                                                        
                                                        ordr[n].status = "Отменён заказчиком";

                                                        Console.Write("Статус заказа изменен на: {0}\n", ordr[n].status);

                                                        ordr.RemoveAt(n);

                                                        break;

                                                    
                                                }

                                            }
                                            k++;
                                        }
                                    }
                                } catch (Exception ex) { }
                            }
                        }

                        break;

                }
            }


            Console.ReadKey();
        }



        void MainMenu()
        {
            string role = "", tmp = "";
            int t;
            try
            {
                do
                {
                    Console.Clear();

                    f2: 

                    Console.Write("1 - Регистрация клиента\n2 - Вход\n0 - Завершение программы\n\n>> ");
                    t = Convert.ToInt32(Console.ReadLine());

                    if (t == 1)
                    {
                        Users usr = new Users();
                        usr.role = "user";

                        do
                        {
                            Console.Write(" ФИО только русские буквы, пробелы и дефисы: ");
                            usr.name = Console.ReadLine();
                        } while (Checker.IsFIOValid(usr.name) == false);

                        do
                        {
                            Console.Write(" Возраст больше 18: ");
                            usr.age = Convert.ToInt32(Console.ReadLine());
                        } while (usr.age < 18 || Checker.IsNumberValid(usr.age.ToString()) == false);

                        usr.salary = 0;
                        do
                        {
                            Console.Write(" Логин: ");
                            usr.login = Console.ReadLine();
                        } while (GetPass(usr.login) != "" || Checker.IsUsernameValid(usr.login) == false);

                        do
                        {
                            Console.Write(" Пароль: ");
                            usr.password = Console.ReadLine();
                        } while (Checker.IsUsernameValid(usr.password) == false);


                        user.Add(usr);
                    }
                    else if (t == 2)
                    {

                        t = 3;

                        while (t > 0)
                        {
                            do
                            {
                                Console.Write("Введите Логин ({0} попыток): ", t);
                                tmp = Console.ReadLine();
                            } while (Checker.IsUsernameValid(tmp) == false);

                            role = FindRole(tmp);
                            if (role == "") t--;
                            else break;
                        }
                        if (t == 0) Console.WriteLine("\nПрограмма будет завершена\n");
                        else
                        {

                            t = 3;
                            string pass = GetPass(tmp);
                            string log = tmp;
                            while (t > 0)
                            {
                                do
                                {
                                    Console.Write("Введите Пароль ({0} попыток): ", t);
                                    tmp = Console.ReadLine();

                                } while (Checker.IsUsernameValid(tmp) == false);

                                if (pass != tmp) t--;
                                else break;
                            }
                            if (t == 0)
                            {
                                Console.WriteLine("\nПрограмма будет завершена\n");
                            }
                            else
                            {
                                switch (role)
                                {
                                    case "admin":
                                        AdminMenu();
                                        break;
                                    case "hr-manager":
                                        HRMenu();
                                        break;
                                    case "dispatcher":
                                        DispatchMenu();
                                        break;
                                    case "driver":
                                        DriverMenu();
                                        break;
                                    case "bookkeeping":
                                        BookMenu();
                                        break;
                                    case "user":
                                        UserMenu(GetInd(log));
                                        break;
                                }
                            }
                        }
                    }
                    Console.ReadKey();




                } while (t != 0);

            }
            catch (Exception ex) { }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            TaxiPark taxiPark = new TaxiPark();
            taxiPark.StartProg();
        }
    }

}

