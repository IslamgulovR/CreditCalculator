using CreditCalculatorV3.Enums;
using CreditCalculatorV3.Models;
using CreditCalculatorV3.Strategies;

namespace CreditCalculatorV3.Services;

public class PaymentScheduleService
{
    public List<Payment> CreatePaymentSchedule(CreateScheduleDto dto)
    {
        return dto.ScheduleType switch
        {
            ScheduleType.Annuity => new AnnuityPaymentScheduleStrategy().CreatePaymentSchedule(dto),
            ScheduleType.Differentiated => new DifferentiationPaymentScheduleStrategy().CreatePaymentSchedule(dto),
            _ => throw new Exception("Schedule type is empty")
        };
    }
}