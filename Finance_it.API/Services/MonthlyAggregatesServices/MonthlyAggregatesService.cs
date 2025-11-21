using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;
using Finance_it.API.Repositories.GenericRepositories;
using Finance_it.API.Services.FinancialAgregatesServices;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Services.MonthlyAgregateServices
{
    public class MonthlyAggregatesService : IMonthlyAggregatesService
    {
        private readonly IGenericRepository<MonthlyAggregate> _monthlyAggregateRepository;
        private readonly IGenericRepository<FinancialEntry> _financialEntryRepository;
        private readonly IMapper _mapper;
        private readonly IFinancialAggregatesService _service;

        public MonthlyAggregatesService(IGenericRepository<MonthlyAggregate> monthlyAggregateRepository, IGenericRepository<FinancialEntry> financialEntryRepository, IMapper mapper, IFinancialAggregatesService service)
        {
            _monthlyAggregateRepository = monthlyAggregateRepository;
            _financialEntryRepository = financialEntryRepository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<CurrentMonthAggregatesDto> GetCurrentMonthAggregatesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var currentDate = DateTime.UtcNow;

            var CurrentMonthEntries = await _financialEntryRepository.GetAllByFilterAsync(
                e => e.UserId == userId &&
                     e.TransactionDate.Month == currentDate.Month &&
                     e.TransactionDate.Year == currentDate.Year, useNoTracking: true, include: q => q.Include(e => e.Category)
            ) ?? throw new NotFoundException("No financial entries found for the current month.");

            decimal totalIncome = _service.TotalIncome(CurrentMonthEntries);                        
            decimal totalExpense = _service.TotalExpense(CurrentMonthEntries);              
            decimal netCashFlow = totalIncome - totalExpense;
            decimal totalSavings = _service.TotalSavings(CurrentMonthEntries);
            decimal netCashFlowRatio = _service.NetCashFlowRatio(CurrentMonthEntries);
            decimal savingsRate = _service.SavingsRate(CurrentMonthEntries);
           

            return new CurrentMonthAggregatesDto
            {
                Year = currentDate.Year,
                Month = currentDate.ToString("MMMM"),
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetCashFlow = netCashFlow,
                TotalSavings = totalSavings,
                NetCashFlowRatio = netCashFlowRatio,
                SavingsRate = savingsRate
            };
        }

        public async Task<IEnumerable<MonthlyAggregateResponseDto>> GetAllMonthlyAggregatesOfTheYearAsync(int userId , DateTime date)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(date, $"the argument {nameof(date)} is null");

            var monthlyAgregates = await _monthlyAggregateRepository.GetAllByFilterAsync(
                m => m.UserId == userId && 
                m.Year == date.Year, useNoTracking: true)?? throw new NotFoundException("No monthly agregates found for the specified year.");

            return _mapper.Map<IEnumerable<MonthlyAggregateResponseDto>>(monthlyAgregates);
        }

        public async Task<IEnumerable<MonthlyAggregateResponseDto>> GetMonthlyAggregatesByMonthAsync(int userId, DateTime date)
        { 
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(date, $"the argument {nameof(date)} is null");

            var monthlyAggregate = await _monthlyAggregateRepository.GetAllByFilterAsync(
                m => m.UserId == userId && 
                m.Month == date.ToString("MMMM") && 
                m.Year == date.Year, useNoTracking: true)?? throw new NotFoundException("No monthly agregate found for the specified month.");

           
            return _mapper.Map<IEnumerable<MonthlyAggregateResponseDto>>(monthlyAggregate);
        } 
    }
}
