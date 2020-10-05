using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static BlazorUITest.Pages.FetchData;

namespace BlazorUITest.Service
{
    public class WeatherService : IWeatherService
    {
      
        public async Task<WeatherForecast[]> GetWeatherDataAsync()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:56692/");
            WeatherForecast[] data = await httpClient.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
            return data;
        }
    }
}
