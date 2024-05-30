using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TeamBugBusters.Models;

namespace TeamBugBusters.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IOptions<WeatherSettings> weatherSettings)
        {
            _httpClient = httpClient;
            _apiKey = weatherSettings.Value.ApiKey;
        }

        public async Task<WeatherInfo> GetWeatherAsync(string city)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(json);


            return weatherInfo;
        }
    }
}
