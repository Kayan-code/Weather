using testeaec.Responses;

namespace testeaec.Repository;

public interface IWeatherAirportRepository
{
    void SaveWeatherAirport(WeatherAirportResponse response);
}