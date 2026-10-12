namespace App.Lab1;

public static class LeapYear
{
    public static bool IsLeapYear(int year) 
    {
        return year % 400 == 0 || (year % 4 == 0 && year % 100 != 0);
    }
}