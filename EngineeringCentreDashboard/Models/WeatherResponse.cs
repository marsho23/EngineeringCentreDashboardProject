using System.Collections.Generic;
using System.Drawing;

namespace EngineeringCentreDashboard.Models
{
    public class WeatherResponse
    {
        public City City { get; set; }
        public List<Forecast> List { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
        public Coord Coord { get; set; }
        public string Country { get; set; }
    }

    public class Coord
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }

    public class Forecast
    {
        public int Dt { get; set; }
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public DateTime NormalDateTimeUtc { get; set; }
        public DateTime NormalDateTimeLocal { get; set; }
    }

    public class RootObject
    {
        public List<Forecast> List { get; set; }
    }


    public class Main
    {
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Clouds
    {
        public int All { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
    }
}
