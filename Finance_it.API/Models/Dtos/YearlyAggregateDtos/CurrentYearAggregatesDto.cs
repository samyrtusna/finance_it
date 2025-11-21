namespace Finance_it.API.Models.Dtos.YearlyAgregateDtos
{
    public class CurrentYearAggregatesDto
    {
        public int Year {  get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetCashFlow { get; set; }
        public decimal TotalSavings { get; set; }
        public decimal FixedExpensesRatio { get; set; }
        public decimal VariableExpensesRatio { get; set; }
        public decimal NetCashFlowRatio { get; set; }
        public decimal SavingsRate { get; set; }
        public decimal DebtToIncomeRatio { get; set; }
    }
}
