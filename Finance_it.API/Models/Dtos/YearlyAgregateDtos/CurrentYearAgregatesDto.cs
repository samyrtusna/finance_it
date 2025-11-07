namespace Finance_it.API.Models.Dtos.YearlyAgregateDtos
{
    public class CurrentYearAgregatesDto
    {
        public int Year {  get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetCashFlow { get; set; }
        public decimal TotalSavings { get; set; }
        public decimal FixedExpenses { get; set; }
        public decimal VariableExpenses { get; set; }
    }
}
