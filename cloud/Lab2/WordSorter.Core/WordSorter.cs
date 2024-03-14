using Newtonsoft.Json;

namespace WordSorter.Core;

public class WordSorter
{
    private static readonly char[] _delimeters = ['.', ',', '!', '?', ' ', '\n'];

    public static string ParseText(string text)
    {
        var result = new SortedDictionary<string, int>();

        var parsedText = text.ToLower().Split(_delimeters, StringSplitOptions.RemoveEmptyEntries);

        foreach ( var c in parsedText )
        {
            if (result.TryAdd(c, 1))
                continue;

            result[c]++;
        }

        return JsonConvert.SerializeObject(result);
    }
}
