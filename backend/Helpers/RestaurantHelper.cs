namespace JustEatAPI.Helpers
{
    public static class RestaurantHelper
    // static because we dont want to create an instance of this class – it’s just a container for helper methods.
    {
        public static string ConstructFullAddress(RestaurantRawModel restaurant)
        {
            var firstLine = restaurant.address?.firstLine ?? string.Empty;
            var city = restaurant.address?.city ?? string.Empty;
            var postalCode = restaurant.address?.postalCode ?? string.Empty;
            // Safely extracts address components — if any are null, falls back to empty string.
            var fullAddress = $"{firstLine}, {city}, {postalCode}".Trim();
            // Combines into a single string, trimming any extra whitespace.
            return string.IsNullOrWhiteSpace(fullAddress) ? "Address not available" : fullAddress;
            // If the entire fullAddress ends up blank or just spaces (e.g., if all fields were empty), it returns "Address not available".
        }
    }
}

// Instead of formatting addresses inside the RestaurantService, we move this logic to a helper, making the service cleaner and more maintainable.