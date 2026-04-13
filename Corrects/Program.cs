using System;
using System.Linq;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using System.ComponentModel;

class User
{
    private bool first = true;
    public string Name { get; set; }
    public string Password { get; set; }
    public string Date {  get; set; }
    public string Path { get; set; }

    public string[] history { get; set; }

    public User(string name, string password, string date, string path)
    {
        Name = name;
        Password = password;
        Date = date;
        Path = path;

        if (File.Exists(path))
        {
            history = File.ReadAllLines(path);
            first = false;
        }

        saveFile();
    }

    private void saveFile()
    {
        if (first == true)
        {
            File.WriteAllText(Path, $"{Name}\n");
            File.AppendAllText(Path, $"{Password}\n");
            File.AppendAllText(Path, $"{Date}\n");
            first = false;
        }
        else
        {
            File.WriteAllLines(Path, history);
        }
        history = File.ReadAllLines(Path);
    }

    public void settings()
    {
        Console.WriteLine("1 - Изменить пароль.");
        Console.WriteLine("2 - Изменить дату рождения.");
        Console.WriteLine("0 - Отмена");
        int user = 0;

        Console.Write("");
        user = Convert.ToInt32(Console.ReadLine());
        if(user == 1)
        {
            Console.Write("Напишите новый пароль: ");
            Password = Console.ReadLine();
        }
        else if(user == 2)
        {
            Console.Write("Напишите новую дату: ");
            Date = Console.ReadLine();
        }
        else
        {
            return;
        }
        saveFile();
        return;
    }

    public void getHistory()
    {
        string[] hist = File.ReadAllLines(Path);

        for(int i = 3; i < hist.Length; i++)
        {
            Console.WriteLine(hist[i]);
        }
    }
}

class Task
{
    public string[] Category { get; } = { "History", "Math", "Eng" };
    public string Name { get; set; }
    public string Path { get; set; }

    public int score { get; set; }

    private List<string> tasks = new List<string>();
    private string[] answer = new string[20];

    private User user { get; set; }

    public Task(string name, string path, User user)
    {
        Name = name;
        Path = path + @$"\tasks";
        score = 0;
        this.user = user;
        Create();
    }

    private void Create()
    {
        string tmpPath = Path;
        // создание директорий под викторины
        if (!Directory.Exists(tmpPath))
        {
            Directory.CreateDirectory(tmpPath);
        }
        // создание директорий под все категории
        for (int i = 0; i < Category.Length; i++)
        {
            tmpPath += @$"\{Category[i]}";
            if (!Directory.Exists(tmpPath))
            {
                Directory.CreateDirectory(tmpPath);
            }
            tmpPath = Path;
        }
        // базовые вопросы/категории
        List<string> History = new List<string>();
        History.Add("В каком году Германия объявила войну Польше?");
        History.Add("Как назывался альянс, основанный Германией?");
        History.Add("В каком году закончилась Советско-Финская война?");
        History.Add("Кто нарушил пакт Молотова-Риббентропа?");
        History.Add("В каком году США вступили в Союзники?");
        History.Add("В каком году Япония напала на Китай?");
        History.Add("Название операции по вторжению Германии в СССР?");
        History.Add("В каком городе был подписан акт о капитуляции Германии?");
        History.Add("На какой японский город была сброшена первая атомная бомба?");
        History.Add("Фамилия правителя СССР?");
        History.Add("Как называлась линия оборонительных укреплений Франции?");
        History.Add("Битва, ставшая коренным переломом на Восточном фронте?");
        History.Add("В каком году произошла высадка союзников в Нормандии?");
        History.Add("Как называлась немецкая шифровальная машина?");
        History.Add("Премьер-министр Великобритании большую часть войны?");
        History.Add("Какое государство было захвачено Германией за 6 часов?");
        History.Add("В каком месяце 1945 года капитулировала Япония?");
        History.Add("Крупнейшее танковое сражение в истории (ухоровка)?");
        History.Add("Столица какого государства была освобождена 9 мая 1945 года?");
        History.Add("Как назывался план нападения Японии на Перл-Харбор?");

        string[] histAnsw = {
    "1939", "Ось", "1940", "Германия", "1941", "1937",
    "Барбаросса", "Берлин", "Хиросима", "Сталин",
    "Мажино", "Сталинградская", "1944", "Энигма",
    "Черчилль", "Дания", "Сентябрь", "Курская",
    "Прага", "Z"
};
        List<string> Math = new List<string>();
        Math.Add("Чему равна площадь квадрата со стороной 5?");
        Math.Add("Корень из 144?");
        Math.Add("Чему равно число Пи (до сотых)?");
        Math.Add("Результат выражения 2 + 2 * 2?");
        Math.Add("Как называется треугольник, у которого все стороны равны?");
        Math.Add("Чему равна сумма углов треугольника?");
        Math.Add("Наименьшее простое число?");
        Math.Add("Чему равен факториал числа 4?");
        Math.Add("Как называется сторона прямоугольного треугольника против прямого угла?");
        Math.Add("Число 10 в нулевой степени?");
        Math.Add("Решите уравнение: 2x - 8 = 0. Чему равен x?");
        Math.Add("Сколько градусов в прямом угле?");
        Math.Add("Как называется отрезок, соединяющий центр окружности с точкой на ней?");
        Math.Add("Чему равна площадь круга, если радиус равен 1?");
        Math.Add("Как называется результат деления?");
        Math.Add("Сколько будет 15% от 200?");
        Math.Add("Найдите медиану ряда чисел: 1, 3, 5, 7, 9?");
        Math.Add("Как называется график функции y = x^2?");
        Math.Add("Чему равен периметр прямоугольника со сторонами 3 и 4?");
        Math.Add("Сколько секунд в одном часе?");
        string[] mathAnsw = {
    "25", "12", "3,14", "6", "Равносторонний", "180",
    "2", "24", "Гипотенуза", "1", "4", "90",
    "Радиус", "3,14", "Частное", "30", "5", "Парабола", "14", "3600"
};
        List<string> Eng = new List<string>();
        Eng.Add("Переведите слово 'Apple' на русский?");
        Eng.Add("Вторая форма глагола 'Go'?");
        Eng.Add("Как на английском будет 'Книга'?");
        Eng.Add("Антоним к слову 'Big'?");
        Eng.Add("Как переводится 'Hello'?");
        Eng.Add("Множественное число слова 'Child'?");
        Eng.Add("Переведите фразу 'I am'?");
        Eng.Add("Как на английском 'Собака'?");
        Eng.Add("Третья форма глагола 'Do'?");
        Eng.Add("Переведите 'Green'?");
        Eng.Add("Как будет 'Учитель' на английском?");
        Eng.Add("Как на английском 'Спасибо'?");
        Eng.Add("Местоимение 'Он'?");
        Eng.Add("Как переводится 'Water'?");
        Eng.Add("Цвет неба на английском?");
        Eng.Add("Цифра 'Семь' на английском?");
        Eng.Add("Как будет 'Мама'?");
        Eng.Add("Переведите слово 'Table'?");
        Eng.Add("Как сказать 'Да'?");
        Eng.Add("Переведите слово 'Friend'?");
        string[] engAnsw = {
    "Яблоко", "Went", "Book", "Small", "Привет", "Children",
    "Я", "Dog", "Done", "Зеленый", "Teacher", "Thanks",
    "He", "Вода", "Blue", "Seven", "Mother", "Стол", "Yes", "Друг"
};

        // заполнение базовых категорий
        tmpPath = Path;
        tmpPath += @$"\{Category[0]}";

        if (!Directory.Exists(tmpPath))
        {
            Directory.CreateDirectory(tmpPath);
        }
        tmpPath += @"\WorldWar2";
        if (!Directory.Exists(tmpPath))
        {
            Directory.CreateDirectory(tmpPath);
        }
        for (int i = 0; i < histAnsw.Length; i++)
        {
            File.WriteAllText(tmpPath + @$"\task{i + 1}.txt", $"{History[i]}\n{histAnsw[i]}");
        }
        if (!File.Exists(tmpPath + @"\top20.txt"))
        {
            File.WriteAllText(tmpPath + @"\top20.txt", "none");
        }

        tmpPath = Path;
        tmpPath += @$"\{Category[1]}";
        if (!Directory.Exists(tmpPath))
        {
            Directory.CreateDirectory(tmpPath);
        }
        tmpPath += @"\Geo&Alg";
        if (!Directory.Exists(tmpPath))
        {
            Directory.CreateDirectory(tmpPath);
        }
        for (int i = 0; i < mathAnsw.Length; i++)
        {
            File.WriteAllText(tmpPath + @$"\task{i + 1}.txt", $"{Math[i]}\n{mathAnsw[i]}");
        }
        if (!File.Exists(tmpPath + @"\top20.txt"))
        {
            File.WriteAllText(tmpPath + @"\top20.txt", "none");
        }

        tmpPath = Path;
        tmpPath += @$"\{Category[2]}";
        if (!Directory.Exists(tmpPath))
        {
            Directory.CreateDirectory(tmpPath);
        }
        tmpPath += @"\Translate";
        if (!Directory.Exists(tmpPath))
        {
            Directory.CreateDirectory(tmpPath);
        }
        for (int i = 0; i < engAnsw.Length; i++)
        {
            File.WriteAllText(tmpPath + @$"\task{i + 1}.txt", $"{Eng[i]}\n{engAnsw[i]}");
        }
        if (!File.Exists(tmpPath + @"\top20.txt"))
        {
            File.WriteAllText(tmpPath + @"\top20.txt", "none");
        }
    }

    public void getCategory()
    {
        for (int i = 0; i < Category.Length; i++)
        {
            Console.WriteLine( Category[i] );
        }
    }

    public void setTask()
    {
        for(int i = 0; i <Category.Length; i++)
        {
            if(Name == Category[i])
            {
                Console.WriteLine("");
                Path += @$"\{Category[i]}";
                int count = 1;
                foreach(string path in Directory.GetDirectories(Path))
                {
                    Console.WriteLine($"{count} {new DirectoryInfo(path).Name}");
                    count++;
                }
                string user = "";
                Console.Write("\nВведите название: ");
                user = Console.ReadLine();
                Path += @$"\{user}";
                if (!File.Exists(Path + @"\task1.txt"))
                {
                    Console.WriteLine("Задания не существует.");
                    return;
                }
                for(int j = 0; j < 20; j++) // заполнение вопросов и ответов
                {
                    tasks.Add(Convert.ToString(File.ReadLines(Path + @$"\task{j + 1}.txt").Skip(0).First()));
                    answer[j] = Convert.ToString(File.ReadLines(Path + @$"\task{j + 1}.txt").Skip(1).First());
                }
            }
        }
    }

    public void Top(string QuizPath)
    {
        string[] top = File.ReadAllLines(QuizPath);
        int j = 1;
        Console.WriteLine();
        for (int i = 1; i < top.Length; i+= 2)
        {
            Console.WriteLine($"{j} - {top[i - 1]} {top[i]}");
            j++;
        }
    }

    public int Top()
    {
        string minUser = "";
        int min = 0;
        int index = 0;

        string tmpPath = Path + @"\top20.txt";

        minUser = File.ReadAllText(tmpPath);

        if (minUser == "none")
        {
            File.WriteAllText(tmpPath, $"{user.Name}\n{score}");
            return 1;
        }
        else
        {
            string[] top = File.ReadAllLines(tmpPath); // 40 - максимум

            for (int i = 1; i < top.Length; i += 2)
            {
                if (min > Convert.ToInt32(top[i]))
                {
                    min = Convert.ToInt32(top[i]);
                    index = i - 1;
                    minUser = top[i - 1];
                }
            }
            if (score > min || top.Length / 2 < 20)
            {
                if (top.Length / 2 < 20)
                {
                    string[] newTop = new string[top.Length + 2]; // создание массива для записи нового человека в топ
                    for (int k = 0; k < top.Length; k++)
                    {
                        newTop[k] = top[k];
                    }

                    newTop[top.Length] = user.Name;
                    newTop[top.Length + 1] = score.ToString();
                    top = newTop;
                    index = top.Length - 2;
                }
                else
                {
                    top[index] = user.Name;
                    top[index + 1] = Convert.ToString(score);
                }

                // bubble sort
                for (int i = 0; i < top.Length; i++)
                {
                    for (int j = 1; j < top.Length - 2; j += 2)
                    {
                        if (Convert.ToInt32(top[j]) < Convert.ToInt32(top[j + 2]))
                        {
                            string tempScore = top[j];
                            top[j] = top[j + 2];
                            top[j + 2] = tempScore;

                            string tempName = top[j - 1];
                            top[j - 1] = top[j + 1];
                            top[j + 1] = tempName;
                        }
                    }
                }

                File.WriteAllLines(tmpPath, top); // новый топ в файл
                for (int i = 0; i < top.Length; i++)
                {
                    if (top[i] == user.Name)
                    {
                        return (i / 2) + 1;
                    }
                }
            }
            else
            {
                return 21;
            }
            return -1;
        }
    }

    public void Run()
    {
        string userAnswer = "";

        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"\nБаллов: {score}");
            Console.Write($"Вопрос {i + 1}: {tasks[i]}\nОтвет: ");
            userAnswer = Console.ReadLine();

            if (userAnswer == answer[i])
            {
                score++;
                Console.WriteLine($"\nВерно!");
            }
            else
            {
                Console.WriteLine($"\nНеверно, правильный ответ: {answer[i]}");
            }
        }
        int win = Top();

        if (win > 20)
        {
            Console.WriteLine($"Всего баллов: {score}");
            Console.WriteLine("Ваше место в викторине: 20+");
        }
        else
        {
            Console.WriteLine($"Всего баллов: {score}");
            Console.WriteLine($"Ваше место в викторине: {win}");
        }

        string[] userFile = File.ReadAllLines(user.Path);

        string[] newUser = new string[userFile.Length + 1];

        for (int i = 0; i < userFile.Length; i++)
        {
            newUser[i] = userFile[i];
        }

        DirectoryInfo di = new DirectoryInfo(Path);

        newUser[userFile.Length] = $"{Name} {di.Name}";

        File.WriteAllLines(user.Path, newUser);
    }
}
class Program
{

    static void CreateBase(string path)
    {
        if (!Directory.Exists(path + @"\users"))
        {
            Directory.CreateDirectory(path + @"\users");
        }

        if(!Directory.Exists(path + @"\tasks"))
        {
            Directory.CreateDirectory(path + @"\tasks");
        }
    }

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
                CreateBase(defaultPath);
                File.WriteAllText(fullPath, defaultPath); // запись директории сохранения файлов
            }
        }
        catch // файла не существует, создаем и просим указать дерективу
        {
            Console.Write("Введите путь, куда сохранять файлы: ");
            defaultPath = Console.ReadLine();
            CreateBase(defaultPath);
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
        string date = "";
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
                                date = File.ReadLines(tmpPath).Skip(2).First();
                                acc = new User(login, answer, date, tmpPath);
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

                        if (File.Exists(tmpPath))
                        {
                            Console.WriteLine("Такой логин уже существует");
                            return;
                        }

                        Console.Write("Введите пароль: ");
                        answer = Console.ReadLine();

                        Console.Write("Введите дату рождения: ");
                        date = Console.ReadLine();

                        acc = new User(login, answer, date, tmpPath);
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
            date = File.ReadLines(accountPath).Skip(2).First();
            acc = new User(login, answer, date, accountPath);
        }

        Task Service = new Task("def", defaultPath, acc);

        while (true)
        {
            Console.WriteLine("\n1 - Пройти викторину");
            Console.WriteLine("2 - Посмотреть историю викторин");
            Console.WriteLine("3 - Топ 20 викторины");
            Console.WriteLine("4 - Изменить настройки аккаунта");
            Console.WriteLine("5 - Выход из аккаунта");
            Console.WriteLine("0 - Выход");
            Console.Write("");
            user = Convert.ToInt32(Console.ReadLine());

            switch (user)
            {
                case 0:
                    {
                        return;
                    }
                case 1:
                    {
                        Console.WriteLine("\nВведите категорию");
                        Service.getCategory();

                        Console.Write("");
                        tmp = Console.ReadLine();

                        Task task = new Task(tmp, defaultPath, acc);
                        task.setTask();
                        task.Run();
                        break;
                    }
                case 2:
                    {
                        acc.getHistory();
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("\nВведите категорию");
                        Service.getCategory();

                        Console.Write("");
                        tmp = Console.ReadLine();
                        tmpPath = defaultPath + @"\tasks\" + tmp;

                        if (!Directory.Exists(tmpPath))
                        {
                            Console.WriteLine("Такой категории не существует.");
                            break;
                        }
                        Console.WriteLine("\nВведите викторину");

                        string[] DirPath = Directory.GetDirectories(tmpPath);

                        for (int i = 0; i < DirPath.Length; i++)
                        {
                            Console.WriteLine(Path.GetFileName(DirPath[i]));
                        }
                        Console.Write("");
                        tmp = Console.ReadLine();
                        tmpPath += @"\" + tmp;

                        if (!Directory.Exists(tmpPath))
                        {
                            Console.WriteLine("Такой викторины не существует.");
                            break;
                        }
                        tmpPath += @"\top20.txt";

                        if (!File.Exists(tmpPath))
                        {
                            Console.WriteLine("Такой викторины не существует 2.");
                            break;
                        }

                        Service.Top(tmpPath);

                        break;
                    }
                case 4:
                    {
                        acc.settings();
                        break;
                    }
                case 5:
                    {
                        File.WriteAllText(lastPath, "none");
                        return;
                    }
            }
        }
    }
}