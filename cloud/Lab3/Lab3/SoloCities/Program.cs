Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Добро пожаловать в игру \"Города\"!");
Console.WriteLine("Давайте начнем!");

var usedCities = new List<string>(); // Список использованных городов
var lastCity = ""; // Последний использованный город

while (true)
{
    Console.Write("Введите название города (или 'exit' для выхода): ");

    // Устанавливаем таймер на 15 секунд
    var timer = new Timer(TimerCallback, null, 15000, Timeout.Infinite);

    var city = Console.ReadLine()
                      .Trim();

    // Если таймер сработал (игрок не ввел город в течение 15 секунд), он проиграл
    if (!string.IsNullOrEmpty(city))
    {
        timer.Change(Timeout.Infinite, Timeout.Infinite); // Отключаем таймер
        timer.Dispose(); // Освобождаем ресурсы таймера
    }
    else
    {
        Console.WriteLine("Время вышло! Вы проиграли!");
        break;
    }

    if (city.ToLower() == "exit")
    {
        Console.WriteLine("Спасибо за игру! До свидания!");
        break;
    }

    if (usedCities.Contains(city))
    {
        Console.WriteLine("Этот город уже был использован. Попробуйте другой.");
        continue;
    }

    if (lastCity != "" && !IsCityValid(city, lastCity))
    {
        Console.WriteLine("Название города некорректно. Попробуйте другой.");
        continue;
    }

    // Добавляем город в список использованных
    usedCities.Add(city);
    // Обновляем последний использованный город
    lastCity = city;

    Console.WriteLine($"Отлично! Ваш город: {city}");
}

bool IsCityValid(string city, string lastCity)
{
    // Проверка существования введенного города в списке существующих городов
    // Здесь предполагается, что список городов находится внутри приложения
    // и доступен для этой проверки
    var allCities = GetListOfAllCities();
    if (!allCities.Select(c => c.ToLower()).Contains(city.ToLower()))
    {
        return false;
    }

    // Проверка на повторение этого города в рамках данной сессии игры
    if (usedCities.Select(c => c.ToLower()).Contains(city))
    {
        return false;
    }

    // Проверка на то, что первая буква в названии города соответствует последней букве предыдущего города
    var lastChar = char.ToLower(lastCity[lastCity.Length - 1]);
    var firstChar = char.ToLower(city[0]);

    if (firstChar != lastChar && !(lastChar == 'ь' && firstChar == lastCity[lastCity.Length - 2]))
    {
        return false;
    }

    return true;
}

static List<string> GetListOfAllCities()
{
    return [
        "Москва",
        "Санкт-Петербург",
        "Нью-Йорк",
        "Лондон",
        "Париж",
        "Берлин",
        "Токио",
        "Пекин",
        "Рим",
        "Мадрид",
        "Вашингтон",
        "Осло",
        "Стокгольм",
        "Афины",
        "Каир",
        "Дели",
        "Бангкок",
        "Мехико",
        "Сидней",
        "Канберра",
        "Оттава",
        "Монреаль",
        "Торонто",
        "Калининград",
        "Варшава",
        "Будапешт",
        "Прага",
        "Вена",
        "Брюссель",
        "Амстердам",
        "Хельсинки",
        "Рейкьявик",
        "Осака",
        "Сеул",
        "Пхеньян",
        "Пхеньян",
        "Стамбул",
        "Анкара",
        "Караганда",
        "Алматы",
        "Астана",
        "Ашхабад",
        "Душанбе",
        "Бишкек",
        "Ташкент",
        "Дакка",
        "Катманду",
        "Сингапур",
        "Джакарта",
        "Бейрут",
        "Дамаск",
        "Абу-Даби",
        "Доха",
        "Мускат",
        "Кувейт",
        "Манама",
        "Рияд",
        "Сана",
        "Тегеран",
        "Багдад",
        "Дамаск",
        "Бейрут",
        "Амман",
        "Иерусалим",
        "Джерусалим",
        "Киев",
        "Минск",
        "Рига",
        "Таллин",
        "Вильнюс",
        "Любляна",
        "Загреб",
        "Скопье",
        "София",
        "Белград",
        "Тирана",
        "Подгорица",
        "Приштина",
        "Сараево",
        "Тирана",
        "Тирасполь",
        "Чисинау",
        "Кишинев",
        "Тбилиси",
        "Ереван",
        "Баку",
        "Ереван",
        "Анкара",
        "Измир",
        "Анталья",
        "Хельсинки",
        "Копенгаген",
        "Осло",
        "Стокгольм",
        "Рейкьявик",
        "Кейптаун",
        "Йоханнесбург",
        "Претория",
        "Дурбан",
        "Лагос",
        "Киншаса",
        "Найроби",
        "Каир",
        "Александрия",
        "Константинополь"
    ];
}


void TimerCallback(Object o)
{
    Console.WriteLine("Время вышло! Вы проиграли!");
    Environment.Exit(0);
}