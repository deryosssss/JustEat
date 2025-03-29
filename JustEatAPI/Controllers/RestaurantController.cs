using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace JustEatAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public RestaurantController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("get-restaurants")]
        public async Task<IActionResult> GetRestaurants([FromQuery] string postcode)
        {
            if (string.IsNullOrEmpty(postcode))
            {
                return BadRequest("Postcode is required.");
            }

            // Clean up the postcode to ensure no spaces
            var url = $"https://uk.api.just-eat.io/discovery/uk/restaurants/enriched/bypostcode/{postcode}";

            try
            {
                // Get the response from the Just Eat API
                var response = await _httpClient.GetStringAsync(url);
                var jsonResponse = JObject.Parse(response);

                // Extract the restaurants from the response (assuming the "restaurants" array is not empty)
                var restaurants = jsonResponse["restaurants"]?
                    .Take(10) // Take only the first 10 restaurants
                    .Where(r => r["name"] != null) // Ensure the restaurant has a name
                    .Select(r => new
                    {
                        Name = r["name"]?.ToString(),
                        Cuisines = string.Join(", ", r["cuisines"]?.Select(c => c["name"]?.ToString()) ?? Enumerable.Empty<string>()),
                        Rating = (double?)r["rating"]?["starRating"] ?? 0, // Default to 0 if rating is not available
                        
                        // Construct the full address including the postcode
                        Address = ConstructFullAddress(r)
                    })
                    .ToList();

                if (restaurants == null || !restaurants.Any())
                {
                    return NotFound("No restaurants found for the given postcode.");
                }

                return Ok(restaurants);
            }
            catch
            {
                return StatusCode(500, "Error fetching restaurant data.");
            }
        }

        private string ConstructFullAddress(JToken restaurant)
        {
            // Extract address parts (with safe null checking)
            var firstLine = restaurant["address"]?["firstLine"]?.ToString() ?? string.Empty;
            var city = restaurant["address"]?["city"]?.ToString() ?? string.Empty;
            var postalCode = restaurant["address"]?["postalCode"]?.ToString() ?? string.Empty;

            // Combine parts to create a full address, excluding the country
            var fullAddress = $"{firstLine}, {city}, {postalCode}".Trim();

            // If all address parts are empty, return a fallback value
            if (string.IsNullOrWhiteSpace(fullAddress))
            {
                fullAddress = "Address not available";
            }

            return fullAddress;
        }
    }
}
