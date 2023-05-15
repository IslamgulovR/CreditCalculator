namespace CreditCalculatorV3.Services;

public static class PayDateService
{
    public static DateTime GetPayDate(int year, int month, int? payday, int daysInMonth)
    {
        return payday > daysInMonth
            ? new DateTime(year, month, daysInMonth, 0, 0, 0)
            : new DateTime(year, month, payday ?? 1, 0, 0, 0);
    }
}