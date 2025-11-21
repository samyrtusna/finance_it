using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;

namespace Finance_it.API.Services.YearlyAgregatesServices
{
    public interface IYearlyAggregatesService
    {
        Task<CurrentYearAggregatesDto> GetCurrentYearAggregatesAsync(int userId);
        Task<IEnumerable<YearlyAggregateResponseDto>> GetAllYearlyAggregatesAsync(int userId);
        Task<IEnumerable<YearlyAggregateResponseDto>> GetYearlyAggregatesByYearAsync(int userId, DateTime date);
    }
}
