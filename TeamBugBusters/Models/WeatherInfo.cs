using TeamBugBusters.Services;

namespace TeamBugBusters.Models
{
    public class WeatherInfo
    {
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
    }
}
