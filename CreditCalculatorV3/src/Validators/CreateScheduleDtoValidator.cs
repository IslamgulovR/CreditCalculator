using CreditCalculatorV3.Models;
using FluentValidation;

namespace CreditCalculatorV3.Validators;

public class CreateScheduleDtoValidator : AbstractValidator<CreateScheduleDto>
{
    public CreateScheduleDtoValidator()
    {
        RuleFor(x => x.DateBegin).Must(d => d > DateTime.UtcNow);
        RuleFor(x => x.DateEnd).GreaterThan(x => x.DateBegin);
        RuleFor(x => Math.Round(x.Months - x.DateEnd.Subtract(x.DateBegin).Days / (365.25 / 12), 0))
            .Equal(0).WithMessage("Months not equal to dateEnd - dateBegin").OverridePropertyName("Months");
        RuleFor(x => x.Percent).InclusiveBetween(0, 100);
    }
}