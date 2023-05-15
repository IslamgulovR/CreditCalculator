using CreditCalculatorV3.Models;
using CreditCalculatorV3.Services;

namespace CreditCalculatorV3.Strategies;

public class DifferentiationPaymentScheduleStrategy : IPaymentScheduleStrategy
{
    public List<Payment> CreatePaymentSchedule(CreateScheduleDto dto)
    {
        var paymentsList = new List<Payment>();
        
        //Сумма погашения основного долга
        var debtSum = Math.Round(dto.Sum / dto.Months, 2);
        var debtLeft = dto.Sum;

        for (var month = 1; month <= dto.Months; month++)
        {
            var nextMonth = dto.DateBegin.AddMonths(month);
            var daysInMonth = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);
            var payDate = PayDateService.GetPayDate(nextMonth.Year, nextMonth.Month, dto.PayDay, daysInMonth);
            var daysInYear = Convert.ToDecimal(DateTime.IsLeapYear(payDate.Year) ? 366 : 365);

            //Сумма платежа по процентам
            var percentSum = Math.Round(debtLeft * dto.Percent / 100 * daysInMonth / daysInYear, 2);

            if (debtSum > debtLeft)
                debtSum = debtLeft;
                
            debtLeft -= debtSum;

            if (month == dto.Months && debtLeft != 0)
            {
                debtSum += debtLeft;
                debtLeft -= debtLeft;
            }
                
            paymentsList.Add(new Payment
            {
                Sum = debtSum + percentSum,
                DebtSum = debtSum,
                PercentSum = percentSum,
                PlanDate = payDate,
                DebtLeft = debtLeft
            });
        }

        return paymentsList.OrderBy(x => x.PlanDate).ToList();
    }
}