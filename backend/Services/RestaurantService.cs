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
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IMapper _mapper;

        public RestaurantService(HttpClient httpClient, ILogger<RestaurantService> logger, IMapper mapper)
        {
            _httpClient = httpClient;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<IEnumerable<RestaurantDto>> GetRestaurantsByPostcodeAsync(string postcode)
        {
            var url = $"https://uk.api.just-eat.io/discovery/uk/restaurants/enriched/bypostcode/{postcode}";

            try
            {
                _logger.LogInformation($"Fetching restaurant data for postcode: {postcode}");
                var response = await _httpClient.GetStringAsync(url);
                var jsonResponse = JObject.Parse(response);

                var restaurantsJson = jsonResponse["restaurants"]?.Take(10);

                if (restaurantsJson == null)
                    return Enumerable.Empty<RestaurantDto>();

                var rawRestaurants = JArray.FromObject(restaurantsJson).ToObject<List<RestaurantRawModel>>();


                var mappedRestaurants = _mapper.Map<List<RestaurantDto>>(rawRestaurants);

                return mappedRestaurants;

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

//  Implement a RestaurantService class to handle API calls and business logic separately. The controller will only manage HTTP requests, leading to cleaner, more modular code.
