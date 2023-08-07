using testeaec.HttpData;
using testeaec.Responses;

namespace testeaec.Services;

public interface IWeatherAirportService
{
    Task<WeatherAirportResult> GetAirportWeatherAsync(WeatherAirportRequest req);
}