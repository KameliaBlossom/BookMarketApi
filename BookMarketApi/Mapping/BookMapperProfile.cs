using AutoMapper;
using BookMarketApi.DTOs;
using BookMarketApi.Model;
using BookMarketApi.Extension;

namespace BookMarketApi.Mapping;


public class BookMapperProfile : Profile
{
    public BookMapperProfile()
    {
        CreateMap<OnlineBook, OnlineBookShortDTO>();

        CreateMap<OnlineBook, OnlineBookDetailDTO>();

        CreateMap<CreateOnlineBookDTO, OnlineBook>();

        CreateMap<UpdateOnlineBookDTO, OnlineBook>();
        
        

        CreateMap<MarketBook, MarketBookShortDTO>();
        CreateMap<MarketBook, MarketBookDetailDTO>();

        CreateMap<CreateMarketBookDTO, MarketBook>()
            .ForMember(
                dest => dest.ListedDate,
                opt => opt.MapFrom(src => DateTime.UtcNow)
            )
            .ForMember(
                dest => dest.ModerationStatus,
                opt => opt.MapFrom(src => ModerationStatus.Pending)
            )
            .ForMember(
                dest => dest.ListingStatus,
                opt => opt.MapFrom(src => ListingStatus.OnModeration)
            );
        
        CreateMap<UpdateMarketBookDTO, MarketBook>();
        
    }
}