using CreditCalculatorV3.Models;
using CreditCalculatorV3.Services;

namespace CreditCalculatorV3.Strategies;

public class AnnuityPaymentScheduleStrategy : IPaymentScheduleStrategy
{
    private const int MonthsInYear = 12;

    public List<Payment> CreatePaymentSchedule(CreateScheduleDto dto)
    {
        var paymentsList = new List<Payment>();

        //Проценты выраженные в дробях
        var percents = dto.Percent / 100;
        
        //Месячная процентная ставка
        var percentRate = Math.Round(percents / 100 * MonthsInYear, 2);
            
        //Ежемесячный платёж
        var annuity = Math.Round(dto.Sum * (percentRate / (decimal)(1.0 - Math.Pow((double)(1 + percentRate), -dto.Months))), 2);
        
        //Сумма долга полная
        var debtLeft = dto.Sum;
        var percentLeft = annuity * dto.Months - dto.Sum;

        for (var month = 1; month <= dto.Months; month++)
        {
            var nextMonth = dto.DateBegin.AddMonths(month);
            var daysInMonth = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);
            var payDate = PayDateService.GetPayDate(nextMonth.Year, nextMonth.Month, dto.PayDay, daysInMonth);
            var daysInYear = Convert.ToDecimal(DateTime.IsLeapYear(payDate.Year)
                                               && payDate <= new DateTime(payDate.Year, 2, 29, 23, 59, 59)
                ? 366
                : 365);

            //Сумма платежа по телу долга
            var percentSum = Math.Round(debtLeft * dto.Percent / 100 * daysInMonth / daysInYear, 2);
            var debtSum = Math.Round(annuity - percentSum, 2);
            debtLeft = Math.Round(debtLeft - debtSum, 2);
            percentLeft = Math.Round(percentLeft - percentSum, 2);

            if (month == dto.Months)
            {
                debtSum -= percentLeft;
                debtLeft += percentLeft;
                annuity = debtSum + percentSum;
            }
            
            paymentsList.Add(new Payment
            {
                Sum = annuity,
                PlanDate = payDate,
                DebtSum = debtSum,
                PercentSum = percentSum,
                DebtLeft = debtLeft
            });
        }

        return paymentsList.OrderBy(x => x.PlanDate).ToList();
    }
}