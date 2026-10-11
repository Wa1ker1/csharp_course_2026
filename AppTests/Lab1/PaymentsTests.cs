using App.Lab1;

namespace AppTests.Lab1;

public class PaymentsTests
{
    [TestCase(PaymentsPlan.Annuity, 7, 3, 10000, 10116.90)]
    [TestCase(PaymentsPlan.Differentiated, 3, 5, 200000, 201500)]
    [TestCase(PaymentsPlan.Annuity, 12, 12, 120000, 127942.20)]
    [TestCase(PaymentsPlan.Differentiated, 12, 12, 120000, 127800.00)]
    [TestCase(PaymentsPlan.Annuity, 10, 6, 60000, 61762.08)]
    [TestCase(PaymentsPlan.Differentiated, 10, 6, 60000, 61750.00)]
    [TestCase(PaymentsPlan.Annuity, 5, 1, 10000, 10041.67)]
    [TestCase(PaymentsPlan.Differentiated, 5, 1, 10000, 10041.67)]
    [TestCase(PaymentsPlan.Annuity, 15, 24, 50000, 58183.92)]
    [TestCase(PaymentsPlan.Differentiated, 15, 24, 50000, 57812.50)]
    public void TestPasses_When_Result_Correct(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount, decimal expected)
    {
        var actual = Payments.CalculateTotalPayments(plan, rate, monthsCount, amount);
        Assert.That(actual, Is.EqualTo(expected));
    }
}