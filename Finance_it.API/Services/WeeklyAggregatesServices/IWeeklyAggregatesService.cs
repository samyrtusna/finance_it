using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;

namespace Finance_it.API.Services.WeeklyAgregateServices
{
    public interface IWeeklyAggregatesService
    {
        Task<CurrentWeekAggregateResponseDto> GetCurrentWeekAggregatesAsync(int userId);
        Task<IEnumerable<WeeklyAggregateResponseDto>> GetAllWeeklyAggregatesOfTheYearAsync(int userId);
        Task<IEnumerable<WeeklyAggregateResponseDto>> GetWeeklyAggregatesByWeekStartDateAsync(int userId, DateTime weekStartDate);
    }
}
  