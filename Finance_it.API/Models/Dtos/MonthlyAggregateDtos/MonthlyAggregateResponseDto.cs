namespace Finance_it.API.Models.Dtos.MonthlyAgregateDtos
{
    public class MonthlyAggregateResponseDto
    {
        public int Year { get; set; }
        public string Month { get; set; } = string.Empty;
        public string AggregateName { get; set; }
        public decimal AggregateValue { get; set; } 
    }
}
