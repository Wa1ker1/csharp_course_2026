namespace App.Lab1;

public enum PaymentsPlan
{
    Differentiated,
    Annuity
}

public static class Payments
{
    public static decimal CalculateTotalPayments(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount)
    {
        decimal monthRate = rate / 100 / 12;

        if (plan == PaymentsPlan.Differentiated)
        {
            decimal mainPayment = amount / monthsCount;
            decimal debt = amount;
            decimal total = 0;

            for (int i = 0; i < monthsCount; i++)
            {
                decimal interest = debt * monthRate;

                total += mainPayment + interest;
                debt -= mainPayment;
            }

            return Math.Round(total, 2);
        }

        decimal coefficient = 1;

        for (int i = 0; i < monthsCount; i++)
        {
            coefficient *= 1 + monthRate;
        }

        decimal monthPayment =
            amount * monthRate * coefficient /
            (coefficient - 1);

        monthPayment = Math.Round(monthPayment, 2);

        return monthPayment * monthsCount;
    }
}