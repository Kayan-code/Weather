using testeaec.Models;
using testeaec.Responses;

namespace testeaec.HttpData;

public class WeatherCityResult
{
    public WeatherCityResult()
    {
        data = new WeatherCityResponse();
        log = new LogResponse();
    }

    /// <summary>
    ///     Response of the query
    /// </summary>
    public WeatherCityResponse data { get; set; }

    /// <summary>
    ///     Log of the query
    /// </summary>
    public LogResponse log { get; set; }
}