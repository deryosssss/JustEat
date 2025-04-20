using JustEatAPI.DTOs;  

namespace JustEatAPI.Services
{
    public interface IRestaurantService
    //  This defines an interface named IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetRestaurantsByPostcodeAsync(string postcode);
        // Task<...> → means it's asynchronous, so it returns a Task that you can await.
        // IEnumerable<RestaurantDto> → it returns a list (or any iterable collection) of RestaurantDto objects.
        // GetRestaurantsByPostcodeAsync(string postcode) → takes a postcode string as input and returns matching restaurants.
    }
}

