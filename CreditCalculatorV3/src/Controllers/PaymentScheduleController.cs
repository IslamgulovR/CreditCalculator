using CreditCalculatorV3.Models;
using CreditCalculatorV3.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditCalculatorV3.Controllers;

[ApiController]
[Route("api/payment-schedule")]
public class PaymentScheduleController : ControllerBase
{
    private readonly PaymentScheduleService _scheduleService;

    public PaymentScheduleController(PaymentScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [ProducesResponseType(typeof(List<Payment>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public ActionResult<List<Payment>> CreatePaymentSchedule([FromBody] CreateScheduleDto dto)
    {
        return Ok(_scheduleService.CreatePaymentSchedule(dto));
    }
}