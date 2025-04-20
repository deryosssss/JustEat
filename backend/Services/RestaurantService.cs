using JustEatAPI.DTOs;
using JustEatAPI.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace JustEatAPI.Services
{
    public class RestaurantService : IRestaurantService
    // This defines the class that implements your service interface.
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IMapper _mapper;
        // These are dependencies injected into the class. They’re readonly because you only set them in the constructor.

        public RestaurantService(HttpClient httpClient, ILogger<RestaurantService> logger, IMapper mapper)
        {
            _httpClient = httpClient;
            _logger = logger;
            _mapper = mapper;
        }
        // The constructor receives the services from Dependency Injection. It assigns them to private fields so the class can use them later.


        public async Task<IEnumerable<RestaurantDto>> GetRestaurantsByPostcodeAsync(string postcode)
        // ^This is the main method. It takes a postcode, fetches restaurants from the Just Eat API, maps them to DTOs, and returns them.
        {
            var url = $"https://uk.api.just-eat.io/discovery/uk/restaurants/enriched/bypostcode/{postcode}";
            // ^Builds the Just Eat API URL with the provided postcode.

            try
            {
                _logger.LogInformation($"Fetching restaurant data for postcode: {postcode}");
                var response = await _httpClient.GetStringAsync(url);
                // Logs the request. Sends a GET request and retrieves the response as a JSON string.
                
                var jsonResponse = JObject.Parse(response);
                var restaurantsJson = jsonResponse["restaurants"]?.Take(10);
                // Parses the JSON string into a JObject. Gets the first 10 restaurants (satisfying your requirement).

                if (restaurantsJson == null)
                    return Enumerable.Empty<RestaurantDto>();

                var rawRestaurants = JArray.FromObject(restaurantsJson).ToObject<List<RestaurantRawModel>>();
                // ^ Converts the 10 JToken restaurant objects to a list of your RestaurantRawModel.

                var mappedRestaurants = _mapper.Map<List<RestaurantDto>>(rawRestaurants);
                // ^ AutoMapper transforms the raw models into cleaned, frontend-ready RestaurantDto objects.

                return mappedRestaurants;
                // This list is returned to the controller → and then to the frontend.

            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while calling Just Eat API.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw;
            }
        }
    }
}
