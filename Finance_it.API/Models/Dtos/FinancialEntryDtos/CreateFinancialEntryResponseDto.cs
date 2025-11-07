using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;

namespace Finance_it.API.Models.Dtos.FinancialEntryDtos
{
    public class CreateFinancialEntryResponseDto
    {
        public CurrentWeekAgregateResponseDto CurrentWeekAgregatesDto { get; set; }
        public CurrentMonthAgregatesDto CurrentMonthAgregatesDto { get; set; }
        public CurrentYearAgregatesDto CurrentYearAgregatesDto { get; set; } 
        public decimal TotalNetCashFlow { get; set; }  
    }  
}
