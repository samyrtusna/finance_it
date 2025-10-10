using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.FinancialEntryDtos;
using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;
using Finance_it.API.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Services
{
    public class FinancialEntryService
    {
        private readonly IGenericRepository<FinancialEntry> _financialEntryRepository;
        private readonly IWeeklyAgregatesService _weeklyAgregateService;
        private readonly IMonthlyAgregatesService _monthlyAgregateService;
        private readonly IYearlyAgregatesService _yearlyAgregatesService;
        private readonly IMapper _mapper;

        public FinancialEntryService(IGenericRepository<FinancialEntry> financialEntryRepository,  
            IMapper mapper, 
            IWeeklyAgregatesService weeklyAgregateService, 
            IMonthlyAgregatesService monthlyAgregateService, 
            IYearlyAgregatesService yearlyAgregatesService)
        {
            _financialEntryRepository = financialEntryRepository;
            _mapper = mapper;
            _weeklyAgregateService = weeklyAgregateService;
            _monthlyAgregateService = monthlyAgregateService;
            _yearlyAgregatesService = yearlyAgregatesService;
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

            var totalIncome = allEntries
                .Where(e => e.Category.Type == FinancialType.Income)
                .Sum(e => e.Amount);

            var totalExpense = allEntries
                .Where(e => e.Category.Type == FinancialType.Expense) 
                .Sum(e => e.Amount);

            var totalBalance = totalIncome - totalExpense;

            return new CreateFinancialEntryResponseDto
            {
                WeeklyAgregateResponseDto = currentWeekAgregates,
                MonthlyAgregateResponseDto = currentMonthAgregates,
                YearlyAgregateResponseDto = currentYearAgregates,
                TotalBalance = totalBalance
            };                                                     
        }

        public async Task<IEnumerable<GetFinancialEntryResponseDto>> GetAllFinancialEntriesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var financialEntries = await _financialEntryRepository.GetAllByFilterAsync(e => e.UserId == userId, include: q => q.Include(e => e.Category));

            return _mapper.Map<IEnumerable<GetFinancialEntryResponseDto>>(financialEntries);
        }

        public async Task<GetFinancialEntryResponseDto> GetFinancialEntryById(int id)
        {
            ArgumentNullException.ThrowIfNull(id, $"the argument {nameof(id)} is null");

            var financialEntry = await _financialEntryRepository.GetByIdAsync(id);

            return _mapper.Map<GetFinancialEntryResponseDto>(financialEntry);
        }

        public async Task<GetFinancialEntryResponseDto> UpdateFinancialEntry(UpdateFinancialEntryRequestDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto, $"The argument {nameof(dto)} is null");

            var financialEntry = await _financialEntryRepository.GetByIdAsync(dto.Id) ?? throw new NotFoundException($"No financial entry found for the Id {dto.Id}");

            var UpdatedEntry = _mapper.Map<FinancialEntry>(dto);

            _financialEntryRepository.Update(UpdatedEntry);
            await _financialEntryRepository.SaveAsync();

            return _mapper.Map<GetFinancialEntryResponseDto>(UpdatedEntry);
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
