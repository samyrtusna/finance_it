using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;

namespace Finance_it.API.Services
{
    public interface IWeeklyAgregatesService
    {
        Task<ConfirmationResponseDto> CreateWeeklyAgregatesAsync(int userId, DateTime date);
        Task<WeeklyAgregateResponseDto> GetCurrentWeekAgregatesAsync(int userId);
        Task<IEnumerable<WeeklyAgregateResponseDto>> GetAllWeeklyAgregatesOfTheYearAsync(int userId);
        Task<WeeklyAgregateResponseDto> GetWeeklyAgregatesByWeekStartDateAsync(int userId, DateTime weekStartDate);
    }
}
  