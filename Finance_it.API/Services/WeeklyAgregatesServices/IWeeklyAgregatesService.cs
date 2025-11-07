using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;

namespace Finance_it.API.Services.WeeklyAgregateServices
{
    public interface IWeeklyAgregatesService
    {
        Task<CurrentWeekAgregateResponseDto> GetCurrentWeekAgregatesAsync(int userId);
        Task<IQueryable<WeeklyAgregateResponseDto>> GetAllWeeklyAgregatesOfTheYearAsync(int userId);
        Task<IQueryable<WeeklyAgregateResponseDto>> GetWeeklyAgregatesByWeekStartDateAsync(int userId, DateTime weekStartDate);
    }
}
  