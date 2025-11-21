using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;

namespace Finance_it.API.Models.Dtos.FinancialEntryDtos
{
    public class CreateFinancialEntryResponseDto
    {
        public CurrentWeekAggregateResponseDto CurrentWeekAggregates { get; set; }
        public CurrentMonthAggregatesDto CurrentMonthAggregates { get; set; }
        public CurrentYearAggregatesDto CurrentYearAggregates { get; set; } 
        public decimal TotalNetCashFlow { get; set; }   
    }  
}
