namespace Finance_it.API.Models.Dtos.MonthlyAgregateDtos
{
    public class MonthlyAgregateResponseDto
    {
        public int Year { get; set; }
        public string Month { get; set; } = string.Empty;
        public string AgregateName { get; set; }
        public decimal AgregateValue { get; set; } 
    }
}
