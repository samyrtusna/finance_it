namespace Finance_it.API.Models.Dtos.MonthlyAgregateDtos
{
    public class MonthlyAgregateResponseDto
    {
        public int Year { get; set; }
        public string Month { get; set; } = string.Empty;
        public decimal MonthIncome { get; set; }
        public decimal MonthExpense { get; set; }
        public decimal MonthBalance { get; set; }  
    }
}
