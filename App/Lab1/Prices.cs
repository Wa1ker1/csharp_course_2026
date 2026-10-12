namespace App.Lab1;

public static class Prices
{
    public static string GetCurrencyAlias(int price, bool isShorNotation, bool isFirstCapital)
    {
        string result; 
        if (isShorNotation)
        {
            result =  "руб";
        }
        else
        {
            int lastTwoDigits = price % 100;
            int lastDigit = price % 10;
            if (lastTwoDigits >= 11 && lastTwoDigits <= 14)
            {
                result = "рублей";
            }
            else if (lastDigit == 1)
            {
                result = "рубль";
            }
            else if (lastDigit >= 2 && lastDigit <= 4)
            {
                result = "рубля";
            }
            else
            {
                result = "рублей";
            }

            if (isFirstCapital)
            {
                result = char.ToUpper(result[0]) + result.Substring(1);
            }
        }
        
        return result;
    }
}