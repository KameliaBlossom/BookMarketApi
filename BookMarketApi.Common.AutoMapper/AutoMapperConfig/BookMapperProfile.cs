using BookMarketApi.Common.Entities.Domain.BookEntities;
using BookMarketApi.Common.Entities.DTOs.OnlineBookDTOs;

namespace BookMarketApi.Common.Automapper.AutoMapperConfig;
using AutoMapper;
public class BookMapperProfile : Profile
{
    public BookMapperProfile()
    {
        CreateMap<OnlineBook, OnlineBookShortDto>();

        CreateMap<OnlineBook, OnlineBookDetailDto>();

        CreateMap<CreateOnlineBookDto, OnlineBook>();

        CreateMap<UpdateOnlineBookDto, OnlineBook>();
    }
}