using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;

namespace Finance_it.API.Services
{
    public interface IYearlyAgregatesService
    {
        Task<ConfirmationResponseDto> CreateYearlyAgregatesAsync(int userId, DateTime date);
        Task<YearlyAgregateResponseDto> GetCurrentYearAgregatesAsync(int userId);
        Task<IEnumerable<YearlyAgregateResponseDto>> GetAllYearlyAgregatesAsync(int userId);
        Task<YearlyAgregateResponseDto> GetYearlyAgregatesByYearAsync(int userId, DateTime date);
    }
}
