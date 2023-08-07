namespace testeaec.Responses;

/// <summary>
///     Class representing weather data for a specific location.
/// </summary>
public class WeatherAirportResponse
{
    /// <summary>
    ///     Humidity percentage.
    /// </summary>
    public int umidade { get; set; }

    /// <summary>
    ///     Visibility in meters.
    /// </summary>
    public string visibilidade { get; set; }

    /// <summary>
    ///     ICAO code for the location.
    /// </summary>
    public string codigo_icao { get; set; }

    /// <summary>
    ///     Atmospheric pressure in hPa.
    /// </summary>
    public int pressao_atmosferica { get; set; }

    /// <summary>
    ///     Wind speed in km/h.
    /// </summary>
    public int vento { get; set; }

    /// <summary>
    ///     Wind direction in degrees.
    /// </summary>
    public int direcao_vento { get; set; }

    /// <summary>
    ///     Weather condition code (e.g., "ps" for Predomínio de Sol).
    /// </summary>
    public string condicao { get; set; }

    /// <summary>
    ///     Description of the weather condition (e.g., "Predomínio de Sol").
    /// </summary>
    public string condicao_desc { get; set; }

    /// <summary>
    ///     Temperature in Celsius.
    /// </summary>
    public int temp { get; set; }

    /// <summary>
    ///     Date and time when the weather data was last updated.
    /// </summary>
    public DateTime atualizado_em { get; set; }
}