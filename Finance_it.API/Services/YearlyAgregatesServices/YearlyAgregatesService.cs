using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;
using Finance_it.API.Repositories.GenericRepositories;
using Finance_it.API.Services.FinancialAgregatesServices;

namespace Finance_it.API.Services.YearlyAgregatesServices
{
    public class YearlyAgregatesService : IYearlyAgregatesService
    {
        private readonly IGenericRepository<FinancialEntry> _financialEntryRepository;
        private readonly IGenericRepository<YearlyAgregate> _yearlyAgregateRepository;
        private readonly IMapper _mapper;
        private readonly IFinancialAgregatesService _service;

        public YearlyAgregatesService(IGenericRepository<FinancialEntry> financialEntryRepository, IGenericRepository<YearlyAgregate> yearlyAgregateRepository, IMapper mapper, IFinancialAgregatesService service)
        {
            _financialEntryRepository = financialEntryRepository;
            _yearlyAgregateRepository = yearlyAgregateRepository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<CurrentYearAgregatesDto> GetCurrentYearAgregatesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            DateTime date = DateTime.UtcNow;

            var currentYearEntries = await _financialEntryRepository.GetAllByFilterAsync(
                e => e.UserId == userId &&
                     e.TransactionDate.Year == date.Year
                ) ?? throw new NotFoundException($"No financial entries found for the year {date.Year} ");

            decimal totalIncome = _service.TotalIncome(currentYearEntries);
            decimal totalExpense = _service.TotalExpense(currentYearEntries);
            decimal netCashFlow = totalIncome - totalExpense;
            decimal totalSavings = _service.TotalSavings(currentYearEntries);
            decimal fixedExpenses = _service.FixedExpenses(currentYearEntries);
            decimal variableExpenses = _service.VariableExpenses(currentYearEntries);

            return new CurrentYearAgregatesDto
            {
                Year = date.Year,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetCashFlow = netCashFlow,
                TotalSavings = totalSavings,
                FixedExpenses = fixedExpenses,
                VariableExpenses = variableExpenses
            };  
        }

        public async Task<IEnumerable<YearlyAgregateResponseDto>> GetAllYearlyAgregatesAsync(int userId)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");

            var yearlyAgregates = await _yearlyAgregateRepository.GetAllByFilterAsync(
                x => x.UserId == userId, useNoTracking: true) ?? throw new NotFoundException("No yearly Agregates found.");

            return _mapper.Map<IEnumerable<YearlyAgregateResponseDto>>(yearlyAgregates);
        }

        public async Task<IEnumerable<YearlyAgregateResponseDto>> GetYearlyAgregatesByYearAsync(int userId, DateTime date)
        {
            ArgumentNullException.ThrowIfNull(userId, $"the argument {nameof(userId)} is null");
            ArgumentNullException.ThrowIfNull(date, $"the argument {nameof(date)} is null");

            var yearlyAgregates = await _yearlyAgregateRepository.GetAllByFilterAsync(
                a => a.UserId == userId &&
                     a.Year == date.Year, useNoTracking: true
                )?? throw new NotFoundException($"No Yearly Agregates found for the year {date.Year}");

            return _mapper.Map<IEnumerable<YearlyAgregateResponseDto>>(yearlyAgregates);
        }
    }
}
