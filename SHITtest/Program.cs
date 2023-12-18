/*Thread t = new Thread(_ =>
{
    while (true)
    {
        Console.SetCursorPosition(0, 2);
        Console.WriteLine("bim bim");
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("bam bam");

        Thread.Sleep(1000);

        Console.SetCursorPosition(0, 0);
        Console.WriteLine("bim bim");
        Console.SetCursorPosition(0, 2);
        Console.WriteLine("bam bam");

        Thread.Sleep(1000);
    }
});
t.Start();*/

/*string shit = "хихи хаха хихи хаха хихи хаха хихи хаха хихи хаха хихи хаха";
Console.WriteLine(shit);
int left = Console.CursorLeft;
int top = Console.CursorTop;
Console.ForegroundColor = ConsoleColor.Cyan;
foreach (var item in shit)
{
    Console.SetCursorPosition(left, top);

    var sumbol = Console.ReadKey(true).KeyChar;
    if(item == sumbol)
    {
        Console.WriteLine(item);
    }
    else
    {
        Console.WriteLine("*");
    }
    left++;
}*/

using Newtonsoft.Json;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

namespace SHITtest
{
    class Programka
    {
        private static List<User> users;
        private static void Main()
        {
            if (!File.Exists("result.json"))
                File.WriteAllText("result.json", "");
            users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("result.json")) ?? new List<User>();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (users.Count > 1) otborUsers();
                ConsoleKeyInfo klavishka;
                do
                {
                    Console.Clear();
                    foreach (var user in users)
                        Console.WriteLine(user.informationAbUsers());
                        Console.WriteLine("\t\t\t ================================================");
                        Console.WriteLine("\t\t\t|\t\t\t\t\t\t |");
                        Console.WriteLine("\t\t\t| хотите попробовать тест на скоропечатанье? y/n |");
                        Console.WriteLine("\t\t\t|\t\t\t\t\t\t |");
                        Console.WriteLine("\t\t\t ================================================");
                        
                        klavishka = Console.ReadKey();
                } 
                while (klavishka.Key != ConsoleKey.Y);
                Test.registration();
                File.WriteAllText("result.json", JsonConvert.SerializeObject(users));
                
            }
        }
        protected static User gotUser(int id)
        {
            foreach (var user in from user in users
                                 where user.IDshnick == id
                                 select user)
            {
                return user;
            }

            return new User();
        }
        protected static bool inUsers(string name)
        {
            foreach (var _ in from user in users
                              where user.imechko == name
                              select new { })
            {
                return true;
            }

            return false;
        }
        protected static void otborUsers()
        {
            User govno = new User();
            for (int i = 0; i < users.Count; i++)
            {
                for (int j = i; j < users.Count - 1; j++)
                {
                    if (users[j].jopaPolnaya > users[j + 1].jopaPolnaya)
                    {
                        govno = users[j];
                        users[j + 1] = users[j];
                        users[j] = govno;
                    }

                }
            }
        }
        protected static void addNewUser(User user) => users.Add(user);
        protected static int getNewIdshnick() => users.Count + 1;
    }
    class User
    {
        public int IDshnick;
        public string imechko;
        public int jopaPolnaya;

        public User()
        {

        }
        public User(int id, string name, int jopapolnaya)
        {
            IDshnick = id;
            imechko = name;
            jopaPolnaya = jopapolnaya;
        }

        public string informationAbUsers() => "Юзер {imechko}\nс результатом:\nсимволов в минутку: {jopaPolnaya}\nсимволов в секундку: {jopaPolnaya / 60}\n";

    }
    class Test : Programka
    {
        private static bool testIsStart = false;
        private static string txt = "Недавно я смотрела фильм Интерстеллар. Буду предельно честна - этот фильм настоящее произведение искусства. Тема космоса интересовала меня с самых ранних лет, и интересует до сих пор. Просмотр этого шедевра пробудил во мне кучу самых необычных, глубоких и воистину выжных мыслей касательно нашего начала и бытия в этом мире. ";
        private static int result = 0, time = 0;
        public static void registration()
        {
            Console.Clear();
            Console.WriteLine("введите ваше имечко:");
            string new_name = Console.ReadLine();
            if (new_name == "") registration();
            if (!inUsers(new_name)) Start(new_name);
            else registration();
        }
        public static void Start(string name)
        {
            ConsoleKeyInfo chlen;
            do
            {
                Console.Clear();
                Console.Write($"{txt}\n\n\n\nкогда будете готовы к тесту - нажмите ENTER"); //тоесть тест своего очка >_<
                chlen = Console.ReadKey(true);
            } 
            while (chlen.Key != ConsoleKey.Enter);
            testIsStart = true;
            new Thread(Timer).Start();
            do
            {
                chlen = Console.ReadKey(true);
                char her = txt[result];
                if (chlen.KeyChar.ToString() == her.ToString())
                {
                    int jopa = result / 120, pospos = result % 120;
                    Console.SetCursorPosition(result % 120, result / 120);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(chlen.KeyChar);
                    result++;
                    if (result - 1 == txt.Length)
                        testIsStart = false;
                }

            } while (testIsStart);
            Console.SetCursorPosition(0, 20);
            Console.WriteLine("штош, тест завершен, Ваш результат сохранился в табличке, всего хорошего ~ ");
            Console.ReadKey(true);
            addNewUser(new User(getNewIdshnick(), name, result / (70 - time) * 70));

        }
        private static void Timer()
        {
            time = 70;
            do
            {
                Console.SetCursorPosition(2, 10);
                Console.WriteLine("    ");
                Console.SetCursorPosition(2, 10);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(time == 70 ? "1:10" : $"0:{time}");
                Thread.Sleep(1000);
                time--;
                if (time == 0) testIsStart = false;
            } while (testIsStart);
        }
    }
}


