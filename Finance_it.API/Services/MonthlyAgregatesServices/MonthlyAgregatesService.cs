using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;
using Finance_it.API.Repositories.GenericRepositories;
using Finance_it.API.Services.FinancialAgregatesServices;

namespace Finance_it.API.Services.MonthlyAgregateServices
{
    public class MonthlyAgregatesService : IMonthlyAgregatesService
    {
        private readonly IGenericRepository<MonthlyAgregate> _monthlyAgregateRepository;
        private readonly IGenericRepository<FinancialEntry> _financialEntryRepository;
        private readonly IMapper _mapper;
        private readonly IFinancialAgregatesService _service;

        public MonthlyAgregatesService(IGenericRepository<MonthlyAgregate> monthlyAgregateRepository, IGenericRepository<FinancialEntry> financialEntryRepository, IMapper mapper, IFinancialAgregatesService service)
        {
            _monthlyAgregateRepository = monthlyAgregateRepository;
            _financialEntryRepository = financialEntryRepository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<CurrentMonthAgregatesDto> GetCurrentMonthAgregatesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var currentDate = DateTime.UtcNow;

            var CurrentMonthEntries = await _financialEntryRepository.GetAllByFilterAsync(
                e => e.UserId == userId &&
                     e.TransactionDate.Month == currentDate.Month &&
                     e.TransactionDate.Year == currentDate.Year, useNoTracking: true
            )?? throw new NotFoundException("No financial entries found for the current month.");

            decimal totalIncome = _service.TotalIncome(CurrentMonthEntries);                        
            decimal totalExpense = _service.TotalExpense(CurrentMonthEntries);              
            decimal netCashFlow = totalIncome - totalExpense;
            decimal totalSavings = _service.TotalSavings(CurrentMonthEntries);
            decimal fixedExpenses = _service.FixedExpenses(CurrentMonthEntries);
            decimal variableExpenses = _service.VariableExpenses(CurrentMonthEntries);

            return new CurrentMonthAgregatesDto
            {
                Year = currentDate.Year,
                Month = currentDate.ToString("MMMM"),
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetCashFlow = netCashFlow,
                TotalSavings = totalSavings,
                FixedExpenses = fixedExpenses,
                VariableExpenses = variableExpenses
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

        public async Task<IEnumerable<MonthlyAgregateResponseDto>> GetMonthlyAgregatesByMonthAsync(int userId, DateTime date)
        { 
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(date, $"the argument {nameof(date)} is null");

            var monthlyAgregate = await _monthlyAgregateRepository.GetAllByFilterAsync(
                m => m.UserId == userId && 
                m.Month == date.ToString("MMMM") && 
                m.Year == date.Year, useNoTracking: true)?? throw new NotFoundException("No monthly agregate found for the specified month.");

           
            return _mapper.Map<IEnumerable<MonthlyAgregateResponseDto>>(monthlyAgregate);
        } 
    }
}
