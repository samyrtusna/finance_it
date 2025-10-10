using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;
using Finance_it.API.Repositories.GenericRepositories;

namespace Finance_it.API.Services
{
    public class MonthlyAgregatesService : IMonthlyAgregatesService
    {
        private readonly IGenericRepository<MonthlyAgregate> _monthlyAgregateRepository;
        private IGenericRepository<FinancialEntry> _financialEntryRepository;
        private readonly IMapper _mapper;

        public MonthlyAgregatesService(IGenericRepository<MonthlyAgregate> monthlyAgregateRepository, IGenericRepository<FinancialEntry> financialEntryRepository, IMapper mapper)
        {
            _monthlyAgregateRepository = monthlyAgregateRepository;
            _financialEntryRepository = financialEntryRepository;
            _mapper = mapper;
        }

        public async Task<ConfirmationResponseDto> CreateMonthlyAgregatesAsync(int userId, DateTime date)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(date, $"the argument {nameof(date)} is null");

            var MonthEntries = await _financialEntryRepository.GetAllByFilterAsync(
                e => e.UserId == userId &&
                     e.TransactionDate.Month == date.Month &&
                     e.TransactionDate.Year == date.Year, useNoTracking: true
            )?? throw new NotFoundException("No financial entries found for the specified month.");

            decimal totalIncome = MonthEntries
                .Where(e => e.Category.Type == FinancialType.Income)
                .Sum(e => e.Amount);

            decimal totalExpense = MonthEntries
                .Where(e => e.Category.Type == FinancialType.Expense)
                .Sum(e => e.Amount);

            decimal balance = totalIncome - totalExpense;

            var monthAgregate = new MonthlyAgregate
            {
                UserId = userId,
                Year = date.Year,
                Month = date.ToString("MMMM"),
                MonthIncome = totalIncome,
                MonthExpense = totalExpense,
                MonthBalance = balance
            };

            await _monthlyAgregateRepository.AddAsync(monthAgregate);
            await _monthlyAgregateRepository.SaveAsync();

            return new ConfirmationResponseDto
            {
                Message = "Monthly agregate created successfully."
            };
        }

        public async Task<MonthlyAgregateResponseDto> GetCurrentMonthAgregatesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var currentDate = DateTime.UtcNow;

            var CurrentMonthEntries = await _financialEntryRepository.GetAllByFilterAsync(
                e => e.UserId == userId &&
                     e.TransactionDate.Month == currentDate.Month &&
                     e.TransactionDate.Year == currentDate.Year, useNoTracking: true
            )?? throw new NotFoundException("No financial entries found for the current month.");

            decimal totalIncome = CurrentMonthEntries
                .Where(e => e.Category.Type == FinancialType.Income)
                .Sum(e => e.Amount);

            decimal totalExpense = CurrentMonthEntries
                .Where(e => e.Category.Type == FinancialType.Expense)
                .Sum(e => e.Amount);

            decimal balance = totalIncome - totalExpense;

            return new MonthlyAgregateResponseDto
            {
                Year = currentDate.Year,
                Month = currentDate.ToString("MMMM"),
                MonthIncome = totalIncome,
                MonthExpense = totalExpense,
                MonthBalance = balance
            };
        }

        public async Task<IEnumerable<MonthlyAgregateResponseDto>> GetAllMonthlyAgregatesOfTheYearAsync(int userId , DateTime date)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(date, $"the argument {nameof(date)} is null");

            var monthlyAgregates = await _monthlyAgregateRepository.GetAllByFilterAsync(
                m => m.UserId == userId && 
                m.Year == date.Year, useNoTracking: true)?? throw new NotFoundException("No monthly agregates found for the specified year.");

            return _mapper.Map<IEnumerable<MonthlyAgregateResponseDto>>(monthlyAgregates);
        }

        public async Task<MonthlyAgregateResponseDto> GetMonthlyAgregatesByMonthAsync(int userId, DateTime date)
        { 
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(date, $"the argument {nameof(date)} is null");

            var monthlyAgregate = await _monthlyAgregateRepository.GetByFilterAsync(
                m => m.UserId == userId && 
                m.Month == date.ToString("MMMM") && 
                m.Year == date.Year, useNoTracking: true)?? throw new NotFoundException("No monthly agregate found for the specified month.");

            if (monthlyAgregate == null)
            {
                return new MonthlyAgregateResponseDto
                {
                    Year = date.Year,
                    Month = date.ToString("MMMM"),
                    MonthIncome = 0,
                    MonthExpense = 0,
                    MonthBalance = 0
                };
            }
            return _mapper.Map<MonthlyAgregateResponseDto>(monthlyAgregate);
        } 
    }
}
