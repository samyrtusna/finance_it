using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;
using Finance_it.API.Repositories.GenericRepositories;
using Finance_it.API.Services.FinancialAgregatesServices;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Services.YearlyAgregatesServices
{
    public class YearlyAggregatesService : IYearlyAggregatesService
    {
        private readonly IGenericRepository<FinancialEntry> _financialEntryRepository;
        private readonly IGenericRepository<YearlyAggregate> _yearlyAggregateRepository;
        private readonly IMapper _mapper;
        private readonly IFinancialAggregatesService _service;

        public YearlyAggregatesService(IGenericRepository<FinancialEntry> financialEntryRepository, IGenericRepository<YearlyAggregate> yearlyAggregateRepository, IMapper mapper, IFinancialAggregatesService service)
        {
            _financialEntryRepository = financialEntryRepository;
            _yearlyAggregateRepository = yearlyAggregateRepository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<CurrentYearAggregatesDto> GetCurrentYearAggregatesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            DateTime date = DateTime.UtcNow;

            var currentYearEntries = await _financialEntryRepository.GetAllByFilterAsync(
                e => e.UserId == userId &&
                     e.TransactionDate.Year == date.Year, include: q => q.Include(e => e.Category), useNoTracking: true
                ) ?? throw new NotFoundException($"No financial entries found for the year {date.Year} ");

            decimal totalIncome = _service.TotalIncome(currentYearEntries);
            decimal totalExpense = _service.TotalExpense(currentYearEntries);
            decimal netCashFlow = totalIncome - totalExpense;
            decimal totalSavings = _service.TotalSavings(currentYearEntries);
            decimal fixedExpensesRatio = _service.FixedExpensesRatio(currentYearEntries);
            decimal variableExpensesRatio = _service.VariableExpensesRatio(currentYearEntries); 
            decimal netCashFlowRatio = _service.NetCashFlowRatio(currentYearEntries);
            decimal savingsRate = _service.SavingsRate(currentYearEntries);
            decimal debtToIncomeRatio = _service.DebtToIncomeRatio(currentYearEntries);

            return new CurrentYearAggregatesDto
            {
                Year = date.Year,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetCashFlow = netCashFlow,
                TotalSavings = totalSavings,
                FixedExpensesRatio = fixedExpensesRatio,
                VariableExpensesRatio = variableExpensesRatio,
                NetCashFlowRatio = netCashFlowRatio,
                SavingsRate = savingsRate,
                DebtToIncomeRatio = debtToIncomeRatio

            };  
        }

        public async Task<IEnumerable<YearlyAggregateResponseDto>> GetAllYearlyAggregatesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var yearlyAggregates = await _yearlyAggregateRepository.GetAllByFilterAsync(
                x => x.UserId == userId, useNoTracking: true) ?? throw new NotFoundException("No yearly Agregates found.");

            return _mapper.Map<IEnumerable<YearlyAggregateResponseDto>>(yearlyAggregates);
        }

        public async Task<IEnumerable<YearlyAggregateResponseDto>> GetYearlyAggregatesByYearAsync(int userId, DateTime date)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(date, $"the argument {nameof(date)} is null");

            var yearlyAgregates = await _yearlyAggregateRepository.GetAllByFilterAsync(
                a => a.UserId == userId &&
                     a.Year == date.Year, useNoTracking: true
                )?? throw new NotFoundException($"No Yearly Agregates found for the year {date.Year}");

            return _mapper.Map<IEnumerable<YearlyAggregateResponseDto>>(yearlyAgregates);
        }
    }
}
