namespace testeaec.Models;

public class Airport
{
    /// <summary>
    /// Gets or sets the unique identifier of the airport.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the ICAO code of the airport.
    /// </summary>
    public string ICAOCode { get; set; }

    /// <summary>
    /// Gets or sets the humidity at the airport.
    /// </summary>
    public int Humidity { get; set; }

    /// <summary>
    /// Gets or sets the visibility at the airport.
    /// </summary>
    public string Visibility { get; set; }

    /// <summary>
    /// Gets or sets the atmospheric pressure at the airport.
    /// </summary>
    public int AtmosphericPressure { get; set; }

    /// <summary>
    /// Gets or sets the wind speed at the airport.
    /// </summary>
    public int WindSpeed { get; set; }

    /// <summary>
    /// Gets or sets the wind direction at the airport.
    /// </summary>
    public int WindDirection { get; set; }

    /// <summary>
    /// Gets or sets the weather condition code at the airport.
    /// </summary>
    public string ConditionCode { get; set; }

    /// <summary>
    /// Gets or sets the weather condition description at the airport.
    /// </summary>
    public string ConditionDescription { get; set; }

    /// <summary>
    /// Gets or sets the temperature at the airport.
    /// </summary>
    public int Temperature { get; set; }

    /// <summary>
    /// Gets or sets the last update date of the airport information.
    /// </summary>
    public DateTime LastUpdated { get; set; }
}