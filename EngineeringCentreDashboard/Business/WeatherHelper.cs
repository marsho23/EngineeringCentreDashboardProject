using EngineeringCentreDashboard.Models;
using Newtonsoft.Json;
using RestSharp;

namespace EngineeringCentreDashboard.Business
{
    public class WeatherHelper
    {

        private readonly string _apiKey;

        private readonly IRestClient _client;
        public WeatherHelper(IConfiguration configuration, IRestClient client)
        {
            _apiKey = configuration.GetValue<string>("WeatherAPIKey");
            _client = client;
        }

        //public List<Forecast> GetForecastForToday(string city)
        //{
        //    var client = new RestClient("http://api.openweathermap.org");
        //    var request = new RestRequest("data/2.5/forecast", Method.GET);
        //    request.AddParameter("q", city);
        //    request.AddParameter("appid", _apiKey);
        //    request.AddParameter("units", "metric");
        //    var response = client.Execute(request);

        //    if (response.IsSuccessful)
        //    {
        //        var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response.Content);
        //        var today = DateTime.Today;
        //        //var tomorrow = today.AddDays(1);
        //        return weatherResponse.List.Where(f =>
        //            DateTimeOffset.FromUnixTimeSeconds(f.Dt).UtcDateTime.Date == today)
        //            .Select(f => new Forecast
        //            {
        //                Dt = f.Dt,
        //                Main = new Main
        //                {
        //                    Temp = f.Main.Temp,
        //                    Feels_like = f.Main.Feels_like,
        //                    Temp_min = f.Main.Temp_min,
        //                    Temp_max = f.Main.Temp_max,
        //                    Pressure = f.Main.Pressure,
        //                    Humidity = f.Main.Humidity
        //                },
        //                Weather = f.Weather.Select(w => new Weather
        //                {
        //                    Id = w.Id,
        //                    Main = w.Main,
        //                    Description = w.Description,
        //                    Icon = w.Icon
        //                }).ToList(),
        //                Clouds = new Clouds
        //                {
        //                    All = f.Clouds.All
        //                },
        //                Wind = new Wind
        //                {
        //                    Speed = f.Wind.Speed,
        //                    Deg = f.Wind.Deg
        //                },
        //                NormalDateTimeUtc = DateTimeOffset.FromUnixTimeSeconds(f.Dt).UtcDateTime,
        //                NormalDateTimeLocal = DateTimeOffset.FromUnixTimeSeconds(f.Dt).ToLocalTime().DateTime

        //            }).ToList();
        //    }
        //    else
        //    {
        //        throw new Exception("Error retrieving weather data: " + response.ErrorMessage);
        //    }
        //}


        public List<Forecast> GetForecastForToday(string city)
        {
            var client = new RestClient("http://api.openweathermap.org");
            var request = new RestRequest("data/2.5/forecast", Method.GET);
            request.AddParameter("q", city);
            request.AddParameter("appid", _apiKey);
            request.AddParameter("units", "metric");
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var weatherResponse = JsonConvert.DeserializeObject<RootObject>(response.Content);
                var today = DateTime.Today;

                return weatherResponse.List.Where(f =>
                    DateTimeOffset.FromUnixTimeSeconds(f.Dt).UtcDateTime.Date == today)
                    .Select(f => new Forecast
                    {
                        Dt = f.Dt,
                        Main = new Main
                        {
                            Temp = f.Main.Temp,
                            Feels_like = f.Main.Feels_like,
                            Temp_min = f.Main.Temp_min,
                            Temp_max = f.Main.Temp_max,
                            Pressure = f.Main.Pressure,
                            Humidity = f.Main.Humidity
                        },
                        Weather = f.Weather.Select(w => new Weather
                        {
                            Id = w.Id,
                            Main = w.Main,
                            Description = w.Description,
                            Icon = w.Icon
                        }).ToList(),
                        Clouds = new Clouds
                        {
                            All = f.Clouds.All
                        },
                        Wind = new Wind
                        {
                            Speed = f.Wind.Speed,
                            Deg = f.Wind.Deg
                        },
                        NormalDateTimeUtc = DateTimeOffset.FromUnixTimeSeconds(f.Dt).UtcDateTime,
                        NormalDateTimeLocal = DateTimeOffset.FromUnixTimeSeconds(f.Dt).ToLocalTime().DateTime

                    }).ToList();
            }
            else
            {
                throw new Exception("Error retrieving weather data: " + response.ErrorMessage);
            }
        }



    }
}