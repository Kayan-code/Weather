namespace testeaec.Models;

public class WeatherCondition
{
    /// <summary>
    /// Gets or sets the unique identifier of the weather condition.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date of the weather condition.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the weather condition code.
    /// </summary>
    public string ConditionCode { get; set; }

    /// <summary>
    /// Gets or sets the weather condition description.
    /// </summary>
    public string ConditionDescription { get; set; }

    /// <summary>
    /// Gets or sets the minimum temperature recorded.
    /// </summary>
    public int MinTemperature { get; set; }

    /// <summary>
    /// Gets or sets the maximum temperature recorded.
    /// </summary>
    public int MaxTemperature { get; set; }

    /// <summary>
    /// Gets or sets the UV index recorded.
    /// </summary>
    public int UVIndex { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the city associated with this weather condition.
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the city associated with this weather condition.
    /// </summary>
    public City City { get; set; }
}