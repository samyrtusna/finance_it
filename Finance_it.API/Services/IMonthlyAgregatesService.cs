using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;

namespace Finance_it.API.Services
{
    public interface IMonthlyAgregatesService
    {
        Task<ConfirmationResponseDto> CreateMonthlyAgregatesAsync(int userId, DateTime date);
        Task<MonthlyAgregateResponseDto> GetCurrentMonthAgregatesAsync(int userId);
        Task<IEnumerable<MonthlyAgregateResponseDto>> GetAllMonthlyAgregatesOfTheYearAsync(int userId, DateTime date);
        Task<MonthlyAgregateResponseDto> GetMonthlyAgregatesByMonthAsync(int UserId, DateTime date); 
    }
}
