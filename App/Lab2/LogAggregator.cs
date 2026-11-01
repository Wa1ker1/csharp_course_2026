namespace App.Lab2;

public class LogAggregator
{
    public static string MergeLogs(params string[] logLines)
    {
        if (logLines == null)
            return "";

        var logs = new List<string>();

        foreach (var line in logLines)
        {
            if (IsValidLog(line))
                logs.Add(line);
        }

        logs.Sort(CompareLogs);

        var groups = new Dictionary<string, List<string>>();
        var traceIds = new List<string>();

        foreach (var line in logs)
        {
            var traceEnd = line.IndexOf(']', 23);
            var traceId = line.Substring(23, traceEnd - 23);

            if (!groups.ContainsKey(traceId))
            {
                groups[traceId] = new List<string>();
                traceIds.Add(traceId);
            }

            var formattedLine = line.Substring(0, 21)  + line.Substring(traceEnd + 1);

            groups[traceId].Add(formattedLine);
        }

        var result = new List<string>();

        foreach (var traceId in traceIds)
        {
            if (result.Count > 0)
                result.Add("");

            result.Add("--- TraceId: [" + traceId + "] ---");

            foreach (var line in groups[traceId])
                result.Add(line);
        }

        return string.Join(Environment.NewLine, result);
    }

    private static int CompareLogs(string first, string second)
    {
        var comparison = string.CompareOrdinal(
            first.Substring(0, 21),
            second.Substring(0, 21));

        if (comparison != 0)
            return comparison;

        var firstEnd = first.IndexOf(']', 23);
        var secondEnd = second.IndexOf(']', 23);

        var firstTrace = first.Substring(23, firstEnd - 23);
        var secondTrace = second.Substring(23, secondEnd - 23);

        comparison = string.CompareOrdinal(firstTrace, secondTrace);

        if (comparison != 0)
            return comparison;

        var firstStart = firstEnd + 2;
        var secondStart = secondEnd + 2;

        var firstService = first.Substring(
            firstStart, first.IndexOf(':', firstStart) - firstStart);

        var secondService = second.Substring(
            secondStart, second.IndexOf(':', secondStart) - secondStart);

        return string.CompareOrdinal(firstService, secondService);
    }

    private static bool IsValidLog(string line)
    {
        if (string.IsNullOrEmpty(line) || line.Length < 30)
            return false;

        if (line[0] != '[' || line[5] != '-' || line[8] != '-'
            || line[11] != ' ' || line[14] != ':' || line[17] != ':'
            || line[20] != ']' || line[21] != ' ' || line[22] != '[')
            return false;

        if (!IsValidTimestamp(line))
            return false;

        if (line.Contains('\n') || line.Contains('\r'))
            return false;

        var traceEnd = line.IndexOf(']', 23);

        if (traceEnd <= 23 || traceEnd + 2 >= line.Length)
            return false;

        for (var i = 23; i < traceEnd; i++)
        {
            if (char.IsWhiteSpace(line[i]) || line[i] == '[')
                return false;
        }

        if (line[traceEnd + 1] != ' ')
            return false;

        var serviceStart = traceEnd + 2;
        var colon = line.IndexOf(':', serviceStart);

        if (colon <= serviceStart || colon + 2 >= line.Length)
            return false;

        if (line[colon + 1] != ' ')
            return false;

        for (var i = serviceStart; i < colon; i++)
        {
            if (char.IsWhiteSpace(line[i]))
                return false;
        }

        return true;
    }

    private static bool IsValidTimestamp(string line)
    {
        for (var i = 1; i < 20; i++)
        {
            if (i == 5 || i == 8 || i == 11 || i == 14 || i == 17)
                continue;

            if (line[i] < '0' || line[i] > '9')
                return false;
        }

        var year = int.Parse(line.Substring(1, 4));
        var month = int.Parse(line.Substring(6, 2));
        var day = int.Parse(line.Substring(9, 2));
        var hour = int.Parse(line.Substring(12, 2));
        var minute = int.Parse(line.Substring(15, 2));
        var second = int.Parse(line.Substring(18, 2));

        if (year == 0 || month < 1 || month > 12)
            return false;

        if (hour > 23 || minute > 59 || second > 59)
            return false;

        var daysInMonth = 31;

        if (month == 4 || month == 6 || month == 9 || month == 11)
            daysInMonth = 30;

        if (month == 2)
        {
            var isLeapYear = year % 400 == 0 || (year % 4 == 0 && year % 100 != 0);

            daysInMonth = isLeapYear ? 29 : 28;
        }

        return day >= 1 && day <= daysInMonth;
    }
}