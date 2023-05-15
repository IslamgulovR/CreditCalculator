using System.Text.Json.Serialization;
using CreditCalculatorV3.Enums;

namespace CreditCalculatorV3.Models;

public class CreateScheduleDto
{
    /// <summary> Сумма кредита </summary>
    public decimal Sum { get; set; }
    
    /// <summary> Годовой процент кредита </summary>
    /// <example>12.0</example>
    public decimal Percent { get; set; }
    
    /// <summary> Дата получения кредита </summary>
    public DateTime DateBegin { get; set; }
    
    /// <summary> Дата выплаты кредита </summary>
    public DateTime DateEnd { get; set; }
    
    /// <summary> Количество месяцев, на которое берётся кредит </summary>
    public int Months { get; set; }
    
    /// <summary> День платежа </summary>
    public int? PayDay { get; set; }
    
    /// <summary> Тип графика платежей </summary>
    //[JsonConverter(typeof(StringEnumConverter))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ScheduleType ScheduleType { get; set; }
}