using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.FinancialEntryDtos;

namespace Finance_it.API.Services.FinancialEntryServices
{
    public interface IFinancialEntryService
    {
        Task<CreateFinancialEntryResponseDto> AddFinancialEntryAsync(CreateFinancialEntryRequestDto entry, int userId);
        Task<IEnumerable<GetFinancialEntryResponseDto>> GetAllFinancialEntriesAsync(int userId);
        Task<GetFinancialEntryResponseDto> GetFinancialEntryByIdAsync(int id);
        Task<GetFinancialEntryResponseDto> UpdateFinancialEntryAsync(UpdateFinancialEntryRequestDto dto, int id);
        Task<ConfirmationResponseDto> DeleteFinancialEntryAsync(int id); 
    }
}
