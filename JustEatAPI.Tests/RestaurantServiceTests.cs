using Xunit;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using JustEatAPI.Services;
using JustEatAPI.DTOs;

namespace JustEatAPI.Tests;

public class RestaurantServiceTests
{
    [Fact]
    public async Task GetRestaurantsByPostcodeAsync_ReturnsRestaurants()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<RestaurantService>>();

        var httpMessageHandler = new Mock<HttpMessageHandler>();
        httpMessageHandler
            .Setup(handler => handler.Send(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{ \"restaurants\": [ { \"name\": \"Test\", \"cuisines\": [{\"name\":\"Pizza\"}], \"rating\": {\"starRating\": 4.5}, \"address\": {\"firstLine\": \"1 Road\", \"city\": \"Town\", \"postalCode\": \"PC1\"} } ] }")
            });

        var httpClient = new HttpClient(httpMessageHandler.Object);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<List<RestaurantDto>>(It.IsAny<object>()))
                  .Returns(new List<RestaurantDto> { new RestaurantDto { Name = "Test", Cuisines = new List<string> { "Pizza" }, Rating = 4.5, Address = "1 Road, Town, PC1" } });

        var service = new RestaurantService(httpClient, loggerMock.Object, mapperMock.Object);

        // Act
        var result = await service.GetRestaurantsByPostcodeAsync("EC1A1BB");

        // Assert
        Assert.Single(result);
        Assert.Equal("Test", result.First().Name);
    }
}

