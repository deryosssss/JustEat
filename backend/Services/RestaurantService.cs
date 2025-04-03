using JustEatAPI.DTOs;  
using JustEatAPI.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace JustEatAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(HttpClient httpClient, ILogger<RestaurantService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<RestaurantDto>> GetRestaurantsByPostcodeAsync(string postcode)
        {
            var url = $"https://uk.api.just-eat.io/discovery/uk/restaurants/enriched/bypostcode/{postcode}";

            try
            {
                _logger.LogInformation($"Fetching restaurant data for postcode: {postcode}");
                var response = await _httpClient.GetStringAsync(url);
                var jsonResponse = JObject.Parse(response);

                var restaurants = jsonResponse["restaurants"]?
                    .Take(10)
                    .Where(r => r["name"] != null)
                    .Select(r => new RestaurantDto
                    {
                        Name = r["name"]?.ToString(),
                        Cuisines = string.Join(", ", r["cuisines"]?.Select(c => c["name"]?.ToString()) ?? Enumerable.Empty<string>()),
                        Rating = (double?)r["rating"]?["starRating"] ?? 0,
                        Address = RestaurantHelper.ConstructFullAddress(r)
                    })
                    .ToList();

                return restaurants ?? Enumerable.Empty<RestaurantDto>();
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

