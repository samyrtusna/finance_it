using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Models.Dtos.FinancialEntryDtos;
using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;
using Finance_it.API.Models.Dtos.RefreshTokenDtos;
using Finance_it.API.Models.Dtos.UserDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;

namespace Finance_it.API.Models.Automapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<RegisterRequestDto, User>()
                .ForMember(Dest => Dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(Dest => Dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(Dest => Dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(Dest => Dest.Role, opt => opt.MapFrom(src => src.Role ?? Role.User))
                .ForMember(Dest => Dest.CreateAt, act => act.Ignore())
                .ForMember(Dest => Dest.LastLogin, act => act.Ignore());

            CreateMap<RefreshTokenRequestDto, RefreshToken>()
                .ForMember(Dest => Dest.Token, opt => opt.MapFrom(src => src.Token))
                .ForMember(Dest => Dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(Dest => Dest.CreatedAt, act => act.Ignore())
                .ForMember(Dest => Dest.ExpiresAt, act => act.Ignore())
                .ForMember(Dest => Dest.IsRevoked, act => act.Ignore())
                .ForMember(Dest => Dest.RevokedAt, act => act.Ignore());

            CreateMap<RefreshToken, RefreshTokenResponseDto>()
                .ForMember(Dest => Dest.Token, opt => opt.MapFrom(src => src.Token))
                .ForMember(Dest => Dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(Dest => Dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(Dest => Dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt))
                .ForMember(Dest => Dest.IsRevoked, opt => opt.MapFrom(src => src.IsRevoked))
                .ForMember(Dest => Dest.RevokedAt, opt => opt.MapFrom(src => src.RevokedAt));

            CreateMap<CreateFinancialEntryRequestDto, FinancialEntry>()
                .ForMember(Dest => Dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(Dest => Dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(Dest => Dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(Dest => Dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(Dest => Dest.TransactionDate, opt => opt.Ignore());

            CreateMap<WeeklyAgregate, WeeklyAgregateResponseDto>()
                .ForMember(Dest => Dest.WeekStartDate, opt => opt.MapFrom(src => src.WeekStartDate))
                .ForMember(Dest => Dest.WeekEndDate, opt => opt.MapFrom(src => src.WeekEndDate))
                .ForMember(dest => dest.WeekIncome, opt => opt.MapFrom(src => src.WeekIncome))
                .ForMember(Dest => Dest.WeekExpense, opt => opt.MapFrom(src => src.WeekExpense))
                .ForMember(Dest => Dest.WeekBalance, opt => opt.MapFrom(src => src.WeekBalance));

            CreateMap<MonthlyAgregate, MonthlyAgregateResponseDto>()
                .ForMember(Dest => Dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(Dest => Dest.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(Dest => Dest.MonthIncome, opt => opt.MapFrom(src => src.MonthIncome))
                .ForMember(Dest => Dest.MonthExpense, opt => opt.MapFrom(src => src.MonthExpense))
                .ForMember(Dest => Dest.MonthBalance, opt => opt.MapFrom(src => src.MonthBalance));

            CreateMap<YearlyAgregate, YearlyAgregateResponseDto>()
                .ForMember(Dest => Dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(Dest => Dest.YearIncome, opt => opt.MapFrom(src => src.YearIncome))
                .ForMember(Dest => Dest.YearExpense, opt => opt.MapFrom(src => src.YearExpense))
                .ForMember(Dest => Dest.YearBalance, opt => opt.MapFrom(src => src.YearBalance));

            CreateMap<FinancialEntry, GetFinancialEntryResponseDto>()
                .ForMember(Dest => Dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(Dest => Dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(Dest => Dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(Dest => Dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(Dest => Dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(Dest => Dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate))
                .ForMember(Dest => Dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<UpdateFinancialEntryRequestDto, FinancialEntry>()
                .ForMember(Dest => Dest.Id, opt => opt.Ignore())
                .ForMember(Dest => Dest.UserId, opt => opt.Ignore())
                .ForMember(Dest => Dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(Dest => Dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(Dest => Dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(Dest => Dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
