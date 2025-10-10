using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;
using Finance_it.API.Repositories.GenericRepositories;

namespace Finance_it.API.Services
{
    public class YearlyAgregatesService : IYearlyAgregatesService
    {
        private readonly IGenericRepository<FinancialEntry> _financialEntryRepository;
        private readonly IGenericRepository<YearlyAgregate> _yearlyAgregateRepository;
        private readonly IMapper _mapper;

        public YearlyAgregatesService(IGenericRepository<FinancialEntry> financialEntryRepository, IGenericRepository<YearlyAgregate> yearlyAgregateRepository, IMapper mapper)
        {
            _financialEntryRepository = financialEntryRepository;
            _yearlyAgregateRepository = yearlyAgregateRepository;
            _mapper = mapper;
        }

        public async Task<ConfirmationResponseDto> CreateYearlyAgregatesAsync(int userId, DateTime date)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(date, $"the argument {nameof(date)} is null");

            var yearEntries = await _financialEntryRepository.GetAllByFilterAsync(
                e => e.UserId == userId &&
                     e.TransactionDate.Year == date.Year, useNoTracking: true
                ) ?? throw new NotFoundException($"No financial entries found for the year {date.Year}");

            decimal totalIncome = yearEntries
                .Where(e => e.Category.Type == FinancialType.Income)
                .Sum(e => e.Amount);

            decimal totalExpense = yearEntries
                .Where(e => e.Category.Type == FinancialType.Expense)
                .Sum(e => e.Amount);

            decimal balance = totalIncome - totalExpense;

            var NewYearlyAgregate = new YearlyAgregate
            {
                UserId = userId,
                Year = date.Year,
                YearIncome = totalIncome,
                YearExpense = totalExpense,
                YearBalance = balance
            };

            await _yearlyAgregateRepository.AddAsync( NewYearlyAgregate );
            await _yearlyAgregateRepository.SaveAsync();

            return new ConfirmationResponseDto
            {
                Message = "Yearly Agregate created successfully."
            };
        }

        public async Task<YearlyAgregateResponseDto> GetCurrentYearAgregatesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            DateTime date = DateTime.UtcNow;

            var CurrentYearEntries = await _financialEntryRepository.GetAllByFilterAsync(
                e => e.UserId == userId &&
                     e.TransactionDate.Year == date.Year
                ) ?? throw new NotFoundException($"No financial entries found for the year {date.Year} ");

            decimal totalIncome = CurrentYearEntries
                .Where(e => e.Category.Type == FinancialType.Income)
                .Sum(e => e.Amount);

            decimal totalExpense = CurrentYearEntries
                .Where(e => e.Category.Type == FinancialType.Expense)
                .Sum(e => e.Amount);

            decimal balance = totalIncome - totalExpense;

            return new YearlyAgregateResponseDto
            {
                Year = date.Year,
                YearIncome = totalIncome,
                YearExpense = totalExpense,
                YearBalance = balance
            }; 
        }

        public async Task<IEnumerable<YearlyAgregateResponseDto>> GetAllYearlyAgregatesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var yearlyAgregates = await _yearlyAgregateRepository.GetAllByFilterAsync(
                x => x.UserId == userId, useNoTracking: true) ?? throw new NotFoundException("No yearly Agregates found.");

            return _mapper.Map<IEnumerable<YearlyAgregateResponseDto>>(yearlyAgregates);
        }

        public async Task<YearlyAgregateResponseDto> GetYearlyAgregatesByYearAsync(int userId, DateTime date)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(date, $"the argument {nameof(date)} is null");

            var yearlyAgregates = await _yearlyAgregateRepository.GetByFilterAsync(
                a => a.UserId == userId &&
                     a.Year == date.Year, useNoTracking: true
                )?? throw new NotFoundException($"No Yearly Agregates found for the year {date.Year}");

            return _mapper.Map<YearlyAgregateResponseDto>(yearlyAgregates);
        }
    }
}
