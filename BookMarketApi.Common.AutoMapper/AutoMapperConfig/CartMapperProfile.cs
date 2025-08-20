using BookMarketApi.Common.Entities.Domain.CartEntities;
using AutoMapper;
using BookMarketApi.Common.Entities.OutputModels.CartOutputModels;

namespace BookMarketApi.Common.Automapper.AutoMapperConfig;

public class CartMapperProfile : Profile
{
    public CartMapperProfile()
    {
        CreateMap<Cart, CartOutputModel>();
    }
}