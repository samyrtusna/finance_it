namespace Finance_it.API.Models.Dtos.WeeklyAgregateDtos
{
    public class CurrentWeekAggregateResponseDto 
    {
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        public decimal WeekIncome { get; set; }
        public decimal WeekExpense { get; set; }
        public decimal WeekNetCashFlow { get; set; }   
    }
}
