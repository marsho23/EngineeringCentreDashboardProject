using EngineeringCentreDashboardWebApp.Models;
using Newtonsoft.Json;
using static EngineeringCentreDashboardWebApp.Models.WeatherResponse;

namespace EngineeringCentreDashboardWebApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse> GetForecastForToday(string city)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7181/api/Weather");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var wrappedResponseString = $"{{ \"List\": {responseString} }}"; // Add the root element "List"

            return JsonConvert.DeserializeObject<WeatherResponse>(wrappedResponseString);
        }

    }
}
