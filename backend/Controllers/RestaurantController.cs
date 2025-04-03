using JustEatAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JustEatAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("get-restaurants")]
        public async Task<IActionResult> GetRestaurants([FromQuery] string postcode)
        {
            if (string.IsNullOrEmpty(postcode))
            {
                return BadRequest("Postcode is required.");
            }

            var restaurants = await _restaurantService.GetRestaurantsByPostcodeAsync(postcode);

            if (!restaurants.Any())
            {
                return NotFound("No restaurants found for the given postcode.");
            }

            return Ok(restaurants);
        }
    }
}
