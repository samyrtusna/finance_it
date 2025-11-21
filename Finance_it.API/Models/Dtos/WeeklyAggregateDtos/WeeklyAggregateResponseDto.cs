using Finance_it.API.Data.Entities;

namespace Finance_it.API.Models.Dtos.WeeklyAgregateDtos
{
    public class WeeklyAggregateResponseDto
    {
        public int UsertId { get; set; }
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        public AggregateName AggregateName { get; set; }
        public decimal AggregateValue { get; set; }
    }
}
