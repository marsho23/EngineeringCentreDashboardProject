using EngineeringCentreDashboard.Business;
using EngineeringCentreDashboard.Controllers;
using EngineeringCentreDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EngineeringCentreDashboard.Tests
{
    public class WeatherControllerTests
    {
        private readonly Mock<IConfiguration> _configMock;
        private readonly Mock<IRestClient> _clientMock;
        private readonly WeatherController _controller;

        public WeatherControllerTests()
        {
            _clientMock = new Mock<IRestClient>();
            _configMock = new Mock<IConfiguration>();

            // Setup RestClient mock to return a successful response
            _clientMock.Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(new RestResponse { StatusCode = System.Net.HttpStatusCode.OK, Content = "{}" });

            // Setup Configuration mock to return an API key
            var configSectionMock = new Mock<IConfigurationSection>();
            configSectionMock.Setup(a => a.Value).Returns("aad4d03ca4620f103c34e6578bdd3500");
            _configMock.Setup(c => c.GetSection("WeatherAPIKey")).Returns(configSectionMock.Object);

            _controller = new WeatherController(_configMock.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_ReturnsExpectedWeather()
        {
            // Act
            var actionResult = _controller.Get();

            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var forecasts = okResult.Value as List<Forecast>;
            Assert.NotNull(forecasts);

            foreach (var forecast in forecasts)
            {
                Assert.NotNull(forecast.Main); // Main should not be null
                Assert.NotNull(forecast.Weather); // Weather should not be null
                Assert.NotNull(forecast.Clouds); // Clouds should not be null
                Assert.NotNull(forecast.Wind); // Wind should not be null

                Assert.IsType<int>(forecast.Dt);
                Assert.All(forecast.Weather, weather =>
                {
                    Assert.IsType<int>(weather.Id);
                    Assert.IsType<string>(weather.Main);
                    Assert.IsType<string>(weather.Description);
                    Assert.IsType<string>(weather.Icon);
                });
                Assert.IsType<int>(forecast.Clouds.All);
                Assert.IsType<double>(forecast.Wind.Speed);
                Assert.IsType<int>(forecast.Wind.Deg);
                Assert.IsType<System.DateTime>(forecast.NormalDateTimeUtc);
                Assert.IsType<System.DateTime>(forecast.NormalDateTimeLocal);
            }
        }
    }
}
