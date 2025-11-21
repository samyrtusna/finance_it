using AutoMapper;
using Finance_it.API.Data.Entities;
using Finance_it.API.Models.Dtos.CategoryDtos;
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
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role ?? Role.User))
                .ForMember(dest => dest.CreateAt, act => act.Ignore())
                .ForMember(dest => dest.LastLogin, act => act.Ignore());

            CreateMap<RefreshTokenRequestDto, RefreshToken>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.Ignore())
                .ForMember(dest => dest.ExpiresAt, act => act.Ignore())
                .ForMember(dest => dest.IsRevoked, act => act.Ignore())
                .ForMember(dest => dest.RevokedAt, act => act.Ignore());

            CreateMap<RefreshToken, RefreshTokenResponseDto>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt))
                .ForMember(dest => dest.IsRevoked, opt => opt.MapFrom(src => src.IsRevoked))
                .ForMember(dest => dest.RevokedAt, opt => opt.MapFrom(src => src.RevokedAt));

            CreateMap<CreateFinancialEntryRequestDto, FinancialEntry>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.TransactionDate, opt => opt.Ignore());

            CreateMap<WeeklyAggregate, WeeklyAggregateResponseDto>()
                .ForMember(dest => dest.UsertId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.WeekStartDate, opt => opt.MapFrom(src => src.WeekStartDate))
                .ForMember(dest => dest.WeekEndDate, opt => opt.MapFrom(src => src.WeekEndDate))
                .ForMember(dest => dest.AggregateName, opt => opt.MapFrom(src => src.AggregateName))
                .ForMember(dest => dest.AggregateValue, opt => opt.MapFrom(src => src.AggregateValue));

            CreateMap<MonthlyAggregate, MonthlyAggregateResponseDto>()
                .ForMember(Dest => Dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(Dest => Dest.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(Dest => Dest.AggregateName, opt => opt.MapFrom(src => src.AggregateName))
                .ForMember(Dest => Dest.AggregateValue, opt => opt.MapFrom(src => src.AggregateValue));

            CreateMap<YearlyAggregate, YearlyAggregateResponseDto>()
                .ForMember(Dest => Dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(Dest => Dest.AggregateName, opt => opt.MapFrom(src => src.AggregateName))
                .ForMember(Dest => Dest.AggregateValue, opt => opt.MapFrom(src => src.AggregateValue));

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

            CreateMap<Category, CategoryResponseDto>()
                .ForMember(Dest => Dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(Dest => Dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(Dest => Dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(Dest => Dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId))
                .ForMember(Dest => Dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));
        }
    }
}
