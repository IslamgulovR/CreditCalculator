namespace CreditCalculatorV3.Models;

public class Payment
{
    /// <summary> Плановая дата платежа </summary>
    public DateTime PlanDate { get; set; }

    /// <summary> Общая сумма платежа </summary>
    public decimal Sum { get; set; }
    
    /// <summary> Сумма платежа по процентам </summary>
    public decimal PercentSum { get; set; }
    
    /// <summary> Сумма платежа по телу кредита </summary>
    public decimal DebtSum { get; set; }
    
    /// <summary> Остаток долга </summary>
    public decimal DebtLeft { get; set; }
}