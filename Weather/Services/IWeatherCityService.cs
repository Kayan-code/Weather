using testeaec.HttpData;
using static Weather.Services.WeatherCityService;

namespace Weather.Services
{
    public interface IWeatherCityService
    {
        Task<List<WeatherCityResult>> GetCityByCityNameWeatherAsync(WeatherCityRequest request);
        Task<WeatherCityResult> GetCityByIdCityWeatherAsync(WeatherCityRequest request);
        Task<List<CityBody>> GetIdCity(string cityName);
    }
}
