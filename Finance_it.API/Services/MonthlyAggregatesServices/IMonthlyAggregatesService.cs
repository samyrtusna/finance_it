using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;

namespace Finance_it.API.Services.MonthlyAgregateServices
{
    public interface IMonthlyAggregatesService
    {
        Task<CurrentMonthAggregatesDto> GetCurrentMonthAggregatesAsync(int userId);
        Task<IEnumerable<MonthlyAggregateResponseDto>> GetAllMonthlyAggregatesOfTheYearAsync(int userId, DateTime date);
        Task<IEnumerable<MonthlyAggregateResponseDto>> GetMonthlyAggregatesByMonthAsync(int userId, DateTime date);
    }
}
