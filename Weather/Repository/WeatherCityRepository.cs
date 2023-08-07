using Weather.Context;
using testeaec.Models;
using testeaec.Responses;
using WeatherCondition = testeaec.Models.WeatherCondition;

namespace testeaec.Repository;

public class WeatherCityRepository : IWeatherCityRepository
{
    private readonly WeatherContext _context;
    private readonly ILogger<WeatherCityRepository> _logger;

    public WeatherCityRepository(WeatherContext context, ILogger<WeatherCityRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void SaveWeatherCities(WeatherCityResponse response)
    {
        try
        {
            var city = _context.Cities.FirstOrDefault(c => c.Name == response.cidade && c.State == response.estado);
            if (city == null)
            {
                city = new City { Name = response.cidade, State = response.estado };
                _context.Cities.Add(city);
            }

            city.LastUpdated = response.atualizado_em;

            foreach (var condition in response.clima)
            {
                var weatherCondition = new WeatherCondition
                {
                    Date = condition.data,
                    ConditionCode = condition.condicao,
                    ConditionDescription = condition.condicao_desc,
                    MinTemperature = condition.min,
                    MaxTemperature = condition.max,
                    UVIndex = condition.indice_uv
                };
                city.Conditions.Add(weatherCondition);
            }

            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving WeatherCityResponse data");
        }
    }
}