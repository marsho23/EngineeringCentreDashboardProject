using EngineeringCentreDashboardWebApp.Models;
using static EngineeringCentreDashboardWebApp.Models.WeatherResponse;

namespace EngineeringCentreDashboardWebApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetForecastForToday(string city);
    }
}
