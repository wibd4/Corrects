using System;
using System.Linq;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

class User
{
    public string Name { get; set; }
    public string Password { get; set; }
    public bool Admin {  get; set; }
    public string Path { get; set; }

    public User(string name, string password, bool admin, string path)
    {
        Name = name;
        Password = password;
        Admin = admin;
        Path = path;
        saveFile();
    }

    private void saveFile()
    {
        File.WriteAllText(Path, $"{Name}\n");
        File.AppendAllText(Path, $"{Password}\n");
        File.AppendAllText(Path, $"{Admin}\n");
    }
}

class Program
{
    static public void Start(ref string defaultPath, ref string fullPath, ref string accountPath, ref string lastPath)
    {
        try
        {
            defaultPath = File.ReadAllText(fullPath); // проверка на существование указанного пользователем пути файла
            if (defaultPath != "none")
            {
                Console.WriteLine(defaultPath);
            }
            else
            {
                Console.Write("Введите путь, куда сохранять файлы: ");
                defaultPath = Console.ReadLine();
                File.WriteAllText(fullPath, defaultPath); // запись директории сохранения файлов
            }
        }
        catch // файла не существует, создаем и просим указать дерективу
        {
            Console.Write("Введите путь, куда сохранять файлы: ");
            defaultPath = Console.ReadLine();
            File.WriteAllText(fullPath, defaultPath);
        }

        try // тоже самое, только для ааунтов
        {
            accountPath = File.ReadAllText(lastPath);
            if (accountPath != "none")
            {
                Console.WriteLine(accountPath);
            }
        }
        catch
        {
            File.WriteAllText(lastPath, "none");
        }
    }

    static void Main(string[] args)
    {
        string defaultPath = "none"; // деректива
        string accountPath = "none"; // последний акк
        string fullPath = Path.GetFullPath("fullpath.txt"); // путь до файла с адресом дерективы
        string lastPath = Path.GetFullPath("lastpath.txt"); // путь до файла с последним акком

        Start(ref defaultPath, ref fullPath, ref accountPath, ref lastPath);

        int user = 0;
        string login = "";
        string answer = "";
        bool admin = false;
        string tmpPath = defaultPath;
        string tmp = "";
        User acc = null;
        if (accountPath == "none")
        {
            Console.WriteLine("1 - Войти в аккаунт\n2 - Зарегистрироваться");
            Console.Write("");
            user = Convert.ToInt32(Console.ReadLine());
            switch (user)
            {
                case 1:
                    {
                        Console.Write("Введите логин: ");
                        login = Console.ReadLine();
                        tmpPath += @$"\users\{login}.txt"; // деректива + login

                        if (File.Exists(tmpPath))
                        {
                            Console.Write("Введите пароль: ");
                            answer = Console.ReadLine();
                            tmp = File.ReadLines(tmpPath).Skip(1).First(); // 2 строчка(пароль)
                            if(answer == tmp)
                            {
                                admin = Convert.ToBoolean(File.ReadLines(tmpPath).Skip(2).First());
                                acc = new User(login, answer, admin, tmpPath);
                                accountPath = tmpPath;
                                File.WriteAllText(lastPath, accountPath);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Неверный пароль.");
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Такого пользователя не существует");
                            return;
                        }
                        break;
                    }
                case 2:
                    {
                        Console.Write("Введите логин: ");
                        login = Console.ReadLine();
                        tmpPath += @$"\users\{login}.txt";

                        Console.Write("Введите пароль: ");
                        answer = Console.ReadLine();

                        acc = new User(login, answer, admin, tmpPath);
                        accountPath = tmpPath;
                        File.WriteAllText(lastPath, accountPath);
                        break;
                    }
            }
        }
        else
        {
            login = File.ReadLines(accountPath).Skip(0).First();
            answer = File.ReadLines(accountPath).Skip(1).First();
            admin = Convert.ToBoolean(File.ReadLines(accountPath).Skip(2).First());
            acc = new User(login, answer, admin, accountPath);
        }

        while (true)
        {

        }
    }
}