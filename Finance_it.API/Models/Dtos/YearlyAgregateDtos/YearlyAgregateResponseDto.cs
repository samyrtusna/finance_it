namespace Finance_it.API.Models.Dtos.YearlyAgregateDtos
{
    public class YearlyAgregateResponseDto
    {
        public int Year { get; set; }
        public decimal YearIncome { get; set; }
        public decimal YearExpense { get; set; } 
        public decimal YearBalance { get; set; } 
    }
}
