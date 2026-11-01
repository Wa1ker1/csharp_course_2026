namespace App.Lab2;

public class Frequency
{
    public static Dictionary<string, string> FrequencyAnalysis(string inputString)
    {
        var frequencies = new Dictionary<string, Dictionary<string, int>>();
        var result = new Dictionary<string, string>();

        var previous = "";
        var beforePrevious = "";
        var start = 0;

        for (var i = 0; i <= inputString.Length; i++)
        {
            if (i < inputString.Length && char.IsLetterOrDigit(inputString[i]))
                continue;

            if (i > start)
            {
                var word = inputString.Substring(start, i - start).ToLowerInvariant();

                if (previous.Length > 0)
                    AddFrequency(frequencies, result, previous, word);

                if (beforePrevious.Length > 0)
                    AddFrequency(frequencies, result, beforePrevious + " " + previous, word);

                beforePrevious = previous;
                previous = word;
            }

            if (i < inputString.Length
                && (inputString[i] == '.' || inputString[i] == '!' || inputString[i] == '?'))
            {
                previous = "";
                beforePrevious = "";
            }

            start = i + 1;
        }

        return result;
    }

    private static void AddFrequency(
        Dictionary<string, Dictionary<string, int>> frequencies,
        Dictionary<string, string> result,
        string prefix,
        string word)
    {
        if (!frequencies.ContainsKey(prefix))
            frequencies[prefix] = new Dictionary<string, int>();

        var counts = frequencies[prefix];

        if (!counts.ContainsKey(word))
            counts[word] = 0;

        counts[word]++;
        var count = counts[word];

        if (!result.ContainsKey(prefix))
        {
            result[prefix] = word;
            return;
        }

        var bestWord = result[prefix];
        var bestCount = counts[bestWord];

        if (count > bestCount || (count == bestCount && string.CompareOrdinal(word, bestWord) < 0))
            result[prefix] = word;
    }
}