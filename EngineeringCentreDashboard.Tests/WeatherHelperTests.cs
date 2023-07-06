using EngineeringCentreDashboard.Business;
using EngineeringCentreDashboard.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace EngineeringCentreDashboard.Tests
{
    public class WeatherHelperTests
    {
        string apiKey = "aad4d03ca4620f103c34e6578bdd3500";

        [Fact]
        public void GetForecastForTodayTest()
        {
            var clientMock = new Mock<IRestClient>();

            // Setup mock for RestClient to return a successful response
            clientMock.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.OK, Content = "{}" });

            var configSectionMock = new Mock<IConfigurationSection>();
            configSectionMock.Setup(a => a.Value).Returns(apiKey);

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c.GetSection("WeatherAPIKey")).Returns(configSectionMock.Object);

            var weatherHelper = new WeatherHelper(configMock.Object, clientMock.Object);

            var result = weatherHelper.GetForecastForToday("Manchester");

            // Verify the returned result
            Assert.NotNull(result); // result should not be null
            Assert.All(result, forecast =>
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
                Assert.IsType<DateTime>(forecast.NormalDateTimeUtc);
                Assert.IsType<DateTime>(forecast.NormalDateTimeLocal);
            });
        }
    }
}
