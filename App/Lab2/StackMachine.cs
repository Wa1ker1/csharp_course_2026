namespace App.Lab2;

public class StackMachine 
{
    public static string CalculateString(string[] codeLines)
    {
        var result = "";

        foreach (var line in codeLines)
        {
            if (line.StartsWith("push "))
            {
                var text = line.Substring(5);
                result += text;
            }
            else if (line.StartsWith("pop "))
            {
                var count = int.Parse(line.Substring(4));
                result = result.Substring(0, result.Length - count);
            }
        }
        
        return result;
    }
}