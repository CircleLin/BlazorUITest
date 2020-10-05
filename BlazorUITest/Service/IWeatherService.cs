using BlazorUITest.Pages;
using System.Threading.Tasks;

namespace BlazorUITest.Service
{
    public interface IWeatherService
    {
        Task<FetchData.WeatherForecast[]> GetWeatherDataAsync();
    }
}