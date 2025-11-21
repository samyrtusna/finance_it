using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Infrastructure.Utils;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;
using Finance_it.API.Repositories.GenericRepositories;
using Finance_it.API.Services.FinancialAgregatesServices;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Services.WeeklyAgregateServices
{
    public class WeeklyAggregatesService : IWeeklyAggregatesService
    {
        private readonly IGenericRepository<WeeklyAggregate> _weeklyAggregateRepository;
        private readonly IGenericRepository<FinancialEntry> _financialRepository;
        private readonly IMapper _mapper;
        private readonly IFinancialAggregatesService _service;

        public WeeklyAggregatesService(IGenericRepository<WeeklyAggregate> repository, IGenericRepository<FinancialEntry> financialRepository, IMapper mapper, IFinancialAggregatesService service)
        {
            _weeklyAggregateRepository = repository;
            _financialRepository = financialRepository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<CurrentWeekAggregateResponseDto> GetCurrentWeekAggregatesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var date = DateTime.UtcNow;
            var week = WeekAggregateUtils.GetWeekStartAndEnd(date);
            var weekStart = week.weekStart;
            var weekEnd = week.weekEnd;

            var weeklyEntries = await _financialRepository.GetAllByFilterAsync(
                e => e.UserId == userId &&
                     e.TransactionDate >= weekStart, useNoTracking: true, include: q => q.Include(e => e.Category)
            ) ?? throw new NotFoundException("No financial entries found for the current week.");

            decimal totalIncome = _service.TotalIncome(weeklyEntries);

            decimal totalExpense = _service.TotalExpense(weeklyEntries);

            decimal netCachFlow = totalIncome - totalExpense;

            return new CurrentWeekAggregateResponseDto
            {
                WeekStartDate = weekStart,
                WeekEndDate = weekEnd,
                WeekIncome = totalIncome,
                WeekExpense = totalExpense,
                WeekNetCashFlow = netCachFlow
            };
        }

        public async Task<IEnumerable<WeeklyAggregateResponseDto>> GetAllWeeklyAggregatesOfTheYearAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var currentYear = DateTime.UtcNow.Year;
            var weeklyAgregates = await _weeklyAggregateRepository.GetAllByFilterAsync(
                w => w.UserId == userId &&
                     w.WeekStartDate.Year == currentYear, useNoTracking: true
            )?? throw new NotFoundException("No weekly agregates found for the current year.");

            return _mapper.Map<IEnumerable<WeeklyAggregateResponseDto>>(weeklyAgregates);
        }

        public async Task<IEnumerable<WeeklyAggregateResponseDto>> GetWeeklyAggregatesByWeekStartDateAsync(int userId, DateTime weekStartDate)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(weekStartDate, $"the argument {nameof(weekStartDate)} is null");

            var weeklyAgregate = await _weeklyAggregateRepository.GetByFilterAsync(
                w => w.UserId == userId &&
                     w.WeekStartDate == weekStartDate, useNoTracking: true
            ) ?? throw new NotFoundException("Weekly agregate not found."); 

            return _mapper.Map<IEnumerable<WeeklyAggregateResponseDto>>(weeklyAgregate);
        }
    }
}
