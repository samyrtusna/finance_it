using Finance_it.API.Data.Entities;

namespace Finance_it.API.Models.Dtos.WeeklyAgregateDtos
{
    public class WeeklyAgregateResponseDto
    {
        public int UsertId { get; set; }
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        public AgregateName AgregateName { get; set; }
        public decimal AgregateValue { get; set; }
    }
}
