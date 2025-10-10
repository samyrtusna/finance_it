using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Infrastructure.Utils;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;
using Finance_it.API.Repositories.GenericRepositories;

namespace Finance_it.API.Services
{
    public class WeeklyAgregatesService : IWeeklyAgregatesService
    {
        private readonly IGenericRepository<WeeklyAgregate> _weeklyAgregateRepository;
        private readonly IGenericRepository<FinancialEntry> _financialRepository;
        private readonly IMapper _mapper;

        public WeeklyAgregatesService(IGenericRepository<WeeklyAgregate> repository, IGenericRepository<FinancialEntry> financialRepository, IMapper mapper)
        {
            _weeklyAgregateRepository = repository;
            _financialRepository = financialRepository;
            _mapper = mapper;
        }

        public async Task<ConfirmationResponseDto> CreateWeeklyAgregatesAsync(int userId, DateTime date)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(date, $"the argument {nameof(date)} is null");

            var week = WeekAgregateUtils.GetWeekStartAndEnd(date);
            var weekStart = week.weekStart;
            var weekEnd = week.weekEnd;

            var weeklyEntries = await _financialRepository.GetAllByFilterAsync(
                e => e.UserId == userId &&
                     e.TransactionDate >= weekStart &&
                     e.TransactionDate < weekEnd, useNoTracking: true
            )?? throw new NotFoundException("No financial entries found for the specified week.");

            decimal totalIncome = weeklyEntries
                .Where(e => e.Category.Type == FinancialType.Income)
                .Sum(e => e.Amount);

            decimal totalExpense = weeklyEntries
                .Where(e => e.Category.Type == FinancialType.Expense)
                .Sum(e => e.Amount);

            decimal balance = totalIncome - totalExpense;

            var weeklyAgregate = new WeeklyAgregate
            {
                UserId = userId,
                WeekStartDate = weekStart,
                WeekEndDate = weekEnd.AddDays(-1),
                WeekIncome = totalIncome,
                WeekExpense = totalExpense,
                WeekBalance = balance
            };

            await _weeklyAgregateRepository.AddAsync(weeklyAgregate);
            await _weeklyAgregateRepository.SaveAsync();

            return new ConfirmationResponseDto
            {
                Message = "Weekly agregate created successfully."
            };
        }

        public async Task<WeeklyAgregateResponseDto> GetCurrentWeekAgregatesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var date = DateTime.UtcNow;
            var week = WeekAgregateUtils.GetWeekStartAndEnd(date);
            var weekStart = week.weekStart;
            var weekEnd = week.weekEnd;

            var weeklyEntries = await _financialRepository.GetAllByFilterAsync(
                e => e.UserId == userId &&
                     e.TransactionDate >= weekStart, useNoTracking: true
            )?? throw new NotFoundException("No financial entries found for the current week.");

            decimal totalIncome = weeklyEntries
                .Where(e => e.Category.Type == FinancialType.Income)
                .Sum(e => e.Amount);

            decimal totalExpense = weeklyEntries
                .Where(e => e.Category.Type == FinancialType.Expense)
                .Sum(e => e.Amount);

            decimal balance = totalIncome - totalExpense;

            return new WeeklyAgregateResponseDto
            {
                WeekStartDate = weekStart,
                WeekEndDate = weekEnd,
                WeekIncome = totalIncome,
                WeekExpense = totalExpense,
                WeekBalance = balance
            };
        }

        public async Task<IEnumerable<WeeklyAgregateResponseDto>> GetAllWeeklyAgregatesOfTheYearAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var currentYear = DateTime.UtcNow.Year;
            var weeklyAgregates = await _weeklyAgregateRepository.GetAllByFilterAsync(
                w => w.UserId == userId &&
                     w.WeekStartDate.Year == currentYear, useNoTracking: true
            )?? throw new NotFoundException("No weekly agregates found for the current year.");

            return _mapper.Map<IEnumerable<WeeklyAgregateResponseDto>>(weeklyAgregates);
        }

        public async Task<WeeklyAgregateResponseDto> GetWeeklyAgregatesByWeekStartDateAsync(int userId, DateTime weekStartDate)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(weekStartDate, $"the argument {nameof(weekStartDate)} is null");

            var weeklyAgregate = await _weeklyAgregateRepository.GetByFilterAsync(
                w => w.UserId == userId &&
                     w.WeekStartDate == weekStartDate, useNoTracking: true
            ) ?? throw new NotFoundException("Weekly agregate not found."); 

            return _mapper.Map<WeeklyAgregateResponseDto>(weeklyAgregate);
        }
    }
}
