using CreditCalculatorV3.Enums;
using CreditCalculatorV3.Models;
using CreditCalculatorV3.Services;
using CreditCalculatorV3.Strategies;
using CreditCalculatorV3.Validators;
using FluentValidation.TestHelper;

namespace Tests;

public class PaymentScheduleTest
{
    [Theory]
    [InlineData(20000.0, 36, 12.0)]
    [InlineData(30000.0, 12, 11.5)]
    [InlineData(15000.0, 18, 15.0)]
    public void AnnuityTest(decimal sum, int months, decimal percent)
    {
        var createScheduleDto = new CreateScheduleDto
        {
            Sum = sum,
            ScheduleType = ScheduleType.Annuity,
            Months = months,
            Percent = percent
        };
        var annuityStrategy = new AnnuityPaymentScheduleStrategy();
        var paymentsList = annuityStrategy.CreatePaymentSchedule(createScheduleDto);
        
        Assert.NotNull(paymentsList);
        
        var paymentsSum = paymentsList.Sum(x => x.DebtSum);
        
        Assert.Equal(sum, paymentsSum);
    }

    [Theory]
    [InlineData(15000.0, 18, 15.0)]
    [InlineData(20000.0, 36, 12.0)]
    [InlineData(37000.0, 13, 11.0)]
    public void DifferentiationTest(decimal sum, int months, decimal percent)
    {
        var createScheduleDto = new CreateScheduleDto
        {
            Sum = sum,
            ScheduleType = ScheduleType.Annuity,
            Months = months,
            Percent = percent
        };
        var annuityStrategy = new DifferentiationPaymentScheduleStrategy();
        var paymentsList = annuityStrategy.CreatePaymentSchedule(createScheduleDto);
        var paymentsSum = paymentsList.Sum(x => x.DebtSum);
        
        Assert.Equal(sum, paymentsSum);
    }

    [Theory]
    [InlineData(15000.0, 18, 15.0, ScheduleType.Annuity)]
    [InlineData(15000.0, 18, 15.0, ScheduleType.Differentiated)]
    [InlineData(15000.0, 18, 15.0, null)]
    public void PaymentSchedule(decimal sum, int months, decimal percent, ScheduleType scheduleType)
    {
        var createScheduleDto = new CreateScheduleDto
        {
            Sum = sum,
            ScheduleType = scheduleType,
            Months = months,
            Percent = percent
        };

        var paymentScheduleService = new PaymentScheduleService();
        var paymentsList = paymentScheduleService.CreatePaymentSchedule(createScheduleDto);
        var paymentsSum = paymentsList.Sum(x => x.DebtSum);
        
        Assert.Equal(sum, paymentsSum);
    }

    [Theory]
    [InlineData(21500.0, 50, 180.9)]
    public void ValidationError(decimal sum, int months, decimal percent)
    {
        var createScheduleDto = new CreateScheduleDto
        {
            Sum = sum,
            Months = months,
            Percent = percent
        };

        var scheduleValidator = new CreateScheduleDtoValidator();
        var result = scheduleValidator.TestValidate(createScheduleDto);

        result.ShouldHaveValidationErrorFor(x => x.DateBegin);
        result.ShouldHaveValidationErrorFor(x => x.DateEnd);
        result.ShouldHaveValidationErrorFor(x => x.Months);
        result.ShouldHaveValidationErrorFor(x => x.Percent);
    }
}