using Grpc.Core;

namespace Cities.Server.Services;

public class CitiesService(ILogger<CitiesService> logger) : CitiesGame.CitiesGameBase
{
    private List<string> usedCities = [];
    private string lastCity = string.Empty;

    public override async Task Play(IAsyncStreamReader<CitiesRequest> requestStream, IServerStreamWriter<CitiesReply> responseStream, ServerCallContext context)
    {
        try
        {
            await responseStream.WriteAsync(new CitiesReply { Message = "Добро пожаловать в игру Города!" });
            await responseStream.WriteAsync(new CitiesReply { Message = "Давайте начнем!" });
            await responseStream.WriteAsync(new CitiesReply { Message = "Введите название города (или 'exit' для выхода): " });

            await foreach (var message in requestStream.ReadAllAsync())
            {
                var city = message.CityName.ToLower();

                if (string.IsNullOrEmpty(city))
                {
                    await responseStream.WriteAsync(new CitiesReply { Message = "Игра окончена" });
                    return;
                }

                if (city.ToLower() == "exit")
                {
                    await responseStream.WriteAsync(new CitiesReply { Message = "Спасибо за игру! До свидания!" });
                    return;
                }

                if (usedCities.Contains(city))
                {
                    await responseStream.WriteAsync(new CitiesReply { Message = "Этот город уже был использован. Попробуйте другой." });
                    continue;
                }

                if (lastCity != string.Empty && !IsCityValid(city, lastCity))
                {
                    await responseStream.WriteAsync(new CitiesReply { Message = "Название города некорректно. Попробуйте другой." });
                    continue;
                }

                usedCities.Add(city);
                lastCity = city;

                await responseStream.WriteAsync(new CitiesReply { Message = $"Отлично! Ваш город: {city}" });
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }

    private void TimerEllapsed(object? state) => throw new Exception("connection aborted with timeout");

    bool IsCityValid(string city, string lastCity)
    {
        var allCities = GetListOfAllCities();
        if (!allCities.Select(c => c.ToLower()).Contains(city.ToLower()))
        {
            return false;
        }

        if (usedCities.Select(c => c.ToLower()).Contains(city))
        {
            return false;
        }

        var lastChar = char.ToLower(lastCity[lastCity.Length - 1]);
        var firstChar = char.ToLower(city[0]);

        return !(firstChar != lastChar && !(lastChar == 'ь' && firstChar == lastCity[lastCity.Length - 2]));
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
}
