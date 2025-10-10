using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;

namespace Finance_it.API.Models.Dtos.FinancialEntryDtos
{
    public class CreateFinancialEntryResponseDto
    {
        public WeeklyAgregateResponseDto WeeklyAgregateResponseDto { get; set; }
        public MonthlyAgregateResponseDto MonthlyAgregateResponseDto { get; set; }
        public YearlyAgregateResponseDto YearlyAgregateResponseDto { get; set; } 
        public decimal TotalBalance { get; set; }
    }  
}
