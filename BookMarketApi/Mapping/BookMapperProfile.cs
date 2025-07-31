namespace BookMarketApi.Mapping;
using AutoMapper;
using BookMarketApi.DTOs;
using BookMarketApi.Model;

public class BookMapperProfile : Profile
{
    public BookMapperProfile()
    {
        CreateMap<OnlineBook, OnlineBookShortDTO>();

        CreateMap<OnlineBook, OnlineBookDetailDTO>();

        CreateMap<CreateOnlineBookDTO, OnlineBook>();

        CreateMap<UpdateOnlineBookDTO, OnlineBook>();
    }
}