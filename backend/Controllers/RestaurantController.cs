using JustEatAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


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

            if (!IsValidUkPostcode(postcode))
            {
                return BadRequest("Invalid UK postcode format.");
            }

            var restaurants = await _restaurantService.GetRestaurantsByPostcodeAsync(postcode);

            if (!restaurants.Any())
            {
                return NotFound("No restaurants found for the given postcode.");
            }

            return Ok(restaurants);

        }
         private bool IsValidUkPostcode(string postcode){
            var pattern = @"^([A-Z]{1,2}[0-9][0-9A-Z]?\s?[0-9][A-Z]{2})$";
            return Regex.IsMatch(postcode, pattern, RegexOptions.IgnoreCase);
         }
    }
}
