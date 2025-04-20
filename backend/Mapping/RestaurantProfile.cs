using AutoMapper;
using JustEatAPI.DTOs;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {

        CreateMap<RestaurantRawModel, RestaurantDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.Cuisines, opt => opt.MapFrom(src => src.cuisines.Select(c => c.name).Where(n => !string.IsNullOrWhiteSpace(n)).ToList()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.address.firstLine}, {src.address.city}, {src.address.postalCode}".Trim()))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.rating.starRating));

    }
}
