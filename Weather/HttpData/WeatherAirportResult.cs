using testeaec.Models;
using testeaec.Responses;

namespace testeaec.HttpData;

public class WeatherAirportResult
{
    public WeatherAirportResult()
    {
        data = new WeatherAirportResponse();
        log = new LogResponse();
    }

    /// <summary>
    ///     Response of the query
    /// </summary>
    public WeatherAirportResponse? data { get; set; }

    /// <summary>
    ///     Log of the query
    /// </summary>
    public LogResponse log { get; set; }
}