namespace Finance_it.API.Models.Dtos.WeeklyAgregateDtos
{
    public class WeeklyAgregateResponseDto 
    {
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        public decimal WeekIncome { get; set; }
        public decimal WeekExpense { get; set; }
        public decimal WeekBalance { get; set; }   
    }
}
