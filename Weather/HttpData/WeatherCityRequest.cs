namespace testeaec.HttpData;

public class WeatherCityRequest
{
    /// <summary>
    ///     Name of the city
    /// </summary>
    public string CityName { get; set; }
    /// <summary>
    ///     Code of the city
    /// </summary>
    public int CityCode { get; set; }
    /// <summary>
    /// Days to return the weather forecast
    /// </summary>
    public int Days { get; set; }
}