using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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

            var url = $"https://uk.api.just-eat.io/discovery/uk/restaurants/enriched/bypostcode/EN35XU";
            
            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var jsonResponse = JObject.Parse(response);

                var restaurants = jsonResponse["restaurants"]
                    .Take(10) // Only take the first 10 restaurants
                    .Select(r => new
                    {
                        Name = r["name"].ToString(),
                        Cuisines = string.Join(", ", r["cuisineTypes"].Select(c => c["name"].ToString())),
                        Rating = (double)r["rating"]["starRating"],
                        Address = r["address"]["firstLine"].ToString()
                    });

                return Ok(restaurants);
            }
            catch
            {
                return StatusCode(500, "Error fetching restaurant data.");
            }
        }
    }
}
