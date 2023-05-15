using CreditCalculatorV3.Models;

namespace CreditCalculatorV3.Strategies;

public interface IPaymentScheduleStrategy
{
    List<Payment> CreatePaymentSchedule(CreateScheduleDto dto);
}