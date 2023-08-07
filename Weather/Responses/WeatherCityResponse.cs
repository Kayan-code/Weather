namespace testeaec.Responses;

public class WeatherCityResponse
{
    /// <summary>
    ///     Name of the city.
    /// </summary>
    public string cidade { get; set; }

    /// <summary>
    ///     State corresponding to the city.
    /// </summary>
    public string estado { get; set; }

    /// <summary>
    ///     Date and time when the weather data was last updated.
    /// </summary>
    public DateTime atualizado_em { get; set; }

    /// <summary>
    ///     List of weather conditions.
    /// </summary>
    public WeatherCondition[] clima { get; set; }
}

/// <summary>
///     Class representing weather conditions for a specific date.
/// </summary>
public class WeatherCondition
{
    /// <summary>
    ///     Date for the weather condition.
    /// </summary>
    public DateTime data { get; set; }

    /// <summary>
    ///     Abbreviated weather condition code (e.g., "pn" for partially cloudy).
    /// </summary>
    public string condicao { get; set; }

    /// <summary>
    ///     Description of the weather condition (e.g., "Partially Cloudy").
    /// </summary>
    public string condicao_desc { get; set; }

    /// <summary>
    ///     Minimum temperature forecast for the day.
    /// </summary>
    public int min { get; set; }

    /// <summary>
    ///     Maximum temperature forecast for the day.
    /// </summary>
    public int max { get; set; }

    /// <summary>
    ///     UV index forecast for the day.
    /// </summary>
    public int indice_uv { get; set; }
}