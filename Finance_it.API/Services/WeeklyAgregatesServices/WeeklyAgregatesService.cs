using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Infrastructure.Utils;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;
using Finance_it.API.Repositories.GenericRepositories;
using Finance_it.API.Services.FinancialAgregatesServices;

namespace Finance_it.API.Services.WeeklyAgregateServices
{
    public class WeeklyAgregatesService : IWeeklyAgregatesService
    {
        private readonly IGenericRepository<WeeklyAgregate> _weeklyAgregateRepository;
        private readonly IGenericRepository<FinancialEntry> _financialRepository;
        private readonly IMapper _mapper;
        private readonly IFinancialAgregatesService _service;

        public WeeklyAgregatesService(IGenericRepository<WeeklyAgregate> repository, IGenericRepository<FinancialEntry> financialRepository, IMapper mapper, IFinancialAgregatesService service)
        {
            _weeklyAgregateRepository = repository;
            _financialRepository = financialRepository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<CurrentWeekAgregateResponseDto> GetCurrentWeekAgregatesAsync(int userId)
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

            decimal totalIncome = _service.TotalIncome(weeklyEntries);

            decimal totalExpense = _service.TotalExpense(weeklyEntries);

            decimal netCachFlow = totalIncome - totalExpense;

            return new CurrentWeekAgregateResponseDto
            {
                WeekStartDate = weekStart,
                WeekEndDate = weekEnd,
                WeekIncome = totalIncome,
                WeekExpense = totalExpense,
                WeekNetCashFlow = netCachFlow
            };
        }

        public async Task<IQueryable<WeeklyAgregateResponseDto>> GetAllWeeklyAgregatesOfTheYearAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var currentYear = DateTime.UtcNow.Year;
            var weeklyAgregates = await _weeklyAgregateRepository.GetAllByFilterAsync(
                w => w.UserId == userId &&
                     w.WeekStartDate.Year == currentYear, useNoTracking: true
            )?? throw new NotFoundException("No weekly agregates found for the current year.");

            return _mapper.Map<IQueryable<WeeklyAgregateResponseDto>>(weeklyAgregates);
        }

        public async Task<IQueryable<WeeklyAgregateResponseDto>> GetWeeklyAgregatesByWeekStartDateAsync(int userId, DateTime weekStartDate)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(weekStartDate, $"the argument {nameof(weekStartDate)} is null");

            var weeklyAgregate = await _weeklyAgregateRepository.GetByFilterAsync(
                w => w.UserId == userId &&
                     w.WeekStartDate == weekStartDate, useNoTracking: true
            ) ?? throw new NotFoundException("Weekly agregate not found."); 

            return _mapper.Map<IQueryable<WeeklyAgregateResponseDto>>(weeklyAgregate);
        }
    }
}
