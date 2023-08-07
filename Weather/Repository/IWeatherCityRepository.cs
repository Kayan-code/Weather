using testeaec.Responses;

namespace testeaec.Repository;

public interface IWeatherCityRepository
{
    void SaveWeatherCities(WeatherCityResponse response);
}