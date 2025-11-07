using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;

namespace Finance_it.API.Services.YearlyAgregatesServices
{
    public interface IYearlyAgregatesService
    {
        Task<CurrentYearAgregatesDto> GetCurrentYearAgregatesAsync(int userId);
        Task<IEnumerable<YearlyAgregateResponseDto>> GetAllYearlyAgregatesAsync(int userId);
        Task<IEnumerable<YearlyAgregateResponseDto>> GetYearlyAgregatesByYearAsync(int userId, DateTime date);
    }
}
