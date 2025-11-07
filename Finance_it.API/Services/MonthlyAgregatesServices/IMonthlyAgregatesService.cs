using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;

namespace Finance_it.API.Services.MonthlyAgregateServices
{
    public interface IMonthlyAgregatesService
    {
        Task<CurrentMonthAgregatesDto> GetCurrentMonthAgregatesAsync(int userId);
        Task<IEnumerable<MonthlyAgregateResponseDto>> GetAllMonthlyAgregatesOfTheYearAsync(int userId, DateTime date);
        Task<IEnumerable<MonthlyAgregateResponseDto>> GetMonthlyAgregatesByMonthAsync(int userId, DateTime date);
    }
}
