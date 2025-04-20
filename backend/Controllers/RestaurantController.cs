using JustEatAPI.Services;
using Microsoft.AspNetCore.Mvc; 
// ^ importing this to be able to us features like controller base 
using System.Threading.Tasks;
// ^ for asyn operations 
using System.Text.RegularExpressions;
// ^ for REGEX

namespace JustEatAPI.Controllers
{
    [ApiController]
    // ^ [ApiController]: Enables some nice automatic features like model validation and automatic binding.
    [Route("api/[controller]")]
    // ^  Defines the base URL path as api/restaurant, because the class is named RestaurantController
    public class RestaurantController : ControllerBase
    // ^ creating a controller class that inherits from the controllerBase as it has methods like badRequest or ok 
    {
        private readonly IRestaurantService _restaurantService;
    // ^ Declares a readonly field that stores the restaurant service. This lets the controller use that service to get data.
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
    // ^ constructor

        [HttpGet("get-restaurants")]
        public async Task<IActionResult> GetRestaurants([FromQuery] string postcode)
        // ^ This method handles HTTP GET requests to /api/restaurant/get-restaurants?postcode=XXXX.
        // The postcode is read from the query string (?postcode=XXXX).
        // The method returns an IActionResult, which can be various HTTP responses (200 OK, 400 Bad Request, etc.).
        // async because it awaits a task from the service.

        {
            if (string.IsNullOrEmpty(postcode))
            {
                return BadRequest("Postcode is required.");
            }
            // ^ If no postcode is provided (it's null or empty), return a 400 Bad Request with a message.

            if (!IsValidUkPostcode(postcode))
            {
                return BadRequest("Invalid UK postcode format.");
            }
            // ^ Uses a helper function to check if the postcode is valid using regex.
            var restaurants = await _restaurantService.GetRestaurantsByPostcodeAsync(postcode);

            if (!restaurants.Any())
            {
                return NotFound("No restaurants found for the given postcode.");
            }

            return Ok(restaurants);
            //  If the list is empty, return a 404 Not Found response.
        }
         private bool IsValidUkPostcode(string postcode){
            var pattern = @"^([A-Z]{1,2}[0-9][0-9A-Z]?\s?[0-9][A-Z]{2})$";
            return Regex.IsMatch(postcode, pattern, RegexOptions.IgnoreCase);
         }
    }
    //  create a function that has a boolean output to figure out if the inputted postcode is a valid uk postcode 
    // Returns true if it matches the pattern, false otherwise.
}
