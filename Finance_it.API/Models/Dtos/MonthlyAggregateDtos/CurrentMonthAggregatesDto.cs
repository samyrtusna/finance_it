namespace Finance_it.API.Models.Dtos.MonthlyAgregateDtos
{
    public class CurrentMonthAggregatesDto
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetCashFlow { get; set; }
        public decimal TotalSavings { get; set; }
        public decimal NetCashFlowRatio { get; set; }
        public decimal SavingsRate { get; set; } 
    }
}
