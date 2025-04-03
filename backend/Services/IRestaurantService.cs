using JustEatAPI.DTOs;  

namespace JustEatAPI.Services
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetRestaurantsByPostcodeAsync(string postcode);
    }
}

