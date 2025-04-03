using Newtonsoft.Json.Linq;

namespace JustEatAPI.Helpers
{
    public static class RestaurantHelper
    {
        public static string ConstructFullAddress(JToken restaurant)
        {
            var firstLine = restaurant["address"]?["firstLine"]?.ToString() ?? string.Empty;
            var city = restaurant["address"]?["city"]?.ToString() ?? string.Empty;
            var postalCode = restaurant["address"]?["postalCode"]?.ToString() ?? string.Empty;

            var fullAddress = $"{firstLine}, {city}, {postalCode}".Trim();
            return string.IsNullOrWhiteSpace(fullAddress) ? "Address not available" : fullAddress;
        }
    }
}

// Instead of formatting addresses inside the RestaurantService, we move this logic to a helper, making the service cleaner and more maintainable.