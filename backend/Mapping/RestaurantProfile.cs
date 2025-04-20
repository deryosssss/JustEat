using AutoMapper;
using JustEatAPI.DTOs;

public class RestaurantProfile : Profile
// Declares a class called RestaurantProfile. It inherits from AutoMapper's Profile class. A Profile is how you configure mapping rules — essentially a map config file for AutoMapper.
{
    public RestaurantProfile()
    {

        CreateMap<RestaurantRawModel, RestaurantDto>()
        // when given a RestaurantRawModel, convert it to a RestaurantDto using these instructions
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
            // Maps the name property from the raw model to the Name property in the DTO.
            .ForMember(dest => dest.Cuisines, opt => opt.MapFrom(src => src.cuisines.Select(c => c.name).Where(n => !string.IsNullOrWhiteSpace(n)).ToList()))
            // Selects the name from each cuisine object, Filters out any null/empty names, Converts the result into a list.
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => RestaurantHelper.ConstructFullAddress(src)))
            // Uses a helper method to format the address neatly
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.rating.starRating));
             // Maps the nested 'starRating' value directly to the Rating field in the DTO.

    }
}
