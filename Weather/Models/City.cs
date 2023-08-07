namespace testeaec.Models;

public class City
{
    /// <summary>
    /// Gets or sets the unique identifier of the city.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the city.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the state where the city is located.
    /// </summary>
    public string State { get; set; }

    /// <summary>
    /// Gets or sets the last update date of the city information.
    /// </summary>
    public DateTime LastUpdated { get; set; }

    /// <summary>
    /// Gets or sets the weather conditions associated with the city.
    /// </summary>
    public ICollection<WeatherCondition> Conditions { get; set; } = new List<WeatherCondition>();
}