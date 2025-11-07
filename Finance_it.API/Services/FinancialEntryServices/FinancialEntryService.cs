using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.FinancialEntryDtos;
using Finance_it.API.Repositories.GenericRepositories;
using Finance_it.API.Services.FinancialAgregatesServices;
using Finance_it.API.Services.MonthlyAgregateServices;
using Finance_it.API.Services.WeeklyAgregateServices;
using Finance_it.API.Services.YearlyAgregatesServices;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Services.FinancialEntryServices
{
    public class FinancialEntryService : IFinancialEntryService
    {
        private readonly IGenericRepository<FinancialEntry> _financialEntryRepository;
        private readonly IWeeklyAgregatesService _weeklyAgregateService;
        private readonly IMonthlyAgregatesService _monthlyAgregateService;
        private readonly IYearlyAgregatesService _yearlyAgregatesService;
        private readonly IMapper _mapper;
        private readonly IFinancialAgregatesService _financialAgregatesService;

        public FinancialEntryService(IGenericRepository<FinancialEntry> financialEntryRepository,  
            IMapper mapper, 
            IWeeklyAgregatesService weeklyAgregateService, 
            IMonthlyAgregatesService monthlyAgregateService, 
            IYearlyAgregatesService yearlyAgregatesService,
            IFinancialAgregatesService financialAgregatesService)
        {
            _financialEntryRepository = financialEntryRepository;
            _mapper = mapper;
            _weeklyAgregateService = weeklyAgregateService;
            _monthlyAgregateService = monthlyAgregateService;
            _yearlyAgregatesService = yearlyAgregatesService;
            _financialAgregatesService = financialAgregatesService;
        }

        public async Task<CreateFinancialEntryResponseDto> AddFinancialEntryAsync(CreateFinancialEntryRequestDto entry)
        {
            ArgumentNullException.ThrowIfNull(entry, $"The argument {nameof(entry)} is null");

            var newEntry = _mapper.Map<FinancialEntry>(entry);

            await _financialEntryRepository.AddAsync(newEntry);
            await _financialEntryRepository.SaveAsync();

            int userId = newEntry.UserId;

            var currentWeekAgregates = await _weeklyAgregateService.GetCurrentWeekAgregatesAsync(userId);
            var currentMonthAgregates = await _monthlyAgregateService.GetCurrentMonthAgregatesAsync(userId);
            var currentYearAgregates = await _yearlyAgregatesService.GetCurrentYearAgregatesAsync(userId);

            var allEntries = await _financialEntryRepository.GetAllByFilterAsync( e => e.UserId == userId, useNoTracking: true);

            decimal totalIncome = _financialAgregatesService.TotalIncome(allEntries);
            decimal totalExpense = _financialAgregatesService.TotalExpense(allEntries);
            decimal totalNetCashFlow = totalIncome - totalExpense;

            return new CreateFinancialEntryResponseDto
            {
                CurrentWeekAgregatesDto = currentWeekAgregates,
                CurrentMonthAgregatesDto = currentMonthAgregates,
                CurrentYearAgregatesDto = currentYearAgregates,
                TotalNetCashFlow = totalNetCashFlow,
            };                                                     
        }

        public async Task<IEnumerable<GetFinancialEntryResponseDto>> GetAllFinancialEntriesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var financialEntries = await _financialEntryRepository.GetAllByFilterAsync(e => e.UserId == userId, include: q => q.Include(e => e.Category));

            return _mapper.Map<IEnumerable<GetFinancialEntryResponseDto>>(financialEntries);
        }

        public async Task<GetFinancialEntryResponseDto> GetFinancialEntryByIdAsync(int id)
        {
            ArgumentNullException.ThrowIfNull(id, $"the argument {nameof(id)} is null");

            var financialEntry = await _financialEntryRepository.GetByIdAsync(id);

            return _mapper.Map<GetFinancialEntryResponseDto>(financialEntry);
        }

        public async Task<GetFinancialEntryResponseDto> UpdateFinancialEntryAsync(UpdateFinancialEntryRequestDto dto, int id)
        {
            ArgumentNullException.ThrowIfNull(dto, $"The argument {nameof(dto)} is null");

            var financialEntry = await _financialEntryRepository.GetByIdAsync(id) ?? throw new NotFoundException($"No financial entry found for the Id {id}");

            _mapper.Map(dto, financialEntry);

            await _financialEntryRepository.SaveAsync();

            return _mapper.Map<GetFinancialEntryResponseDto>(financialEntry);
        }

        public async Task<ConfirmationResponseDto> DeleteFinancialEntryAsync(int id)
        {
            ArgumentNullException.ThrowIfNull(id, $"the argument {nameof(id)} is null");

            var financialEntry = await _financialEntryRepository.GetByIdAsync(id)?? throw new NotFoundException($"No financial entry found for the Id {id}");

            _financialEntryRepository.Delete(financialEntry);
            await _financialEntryRepository.SaveAsync();

            return new ConfirmationResponseDto
            {
                Message = $"The Financial entry with Id {id} is deleted successfully."
            };
        }
    }
}
