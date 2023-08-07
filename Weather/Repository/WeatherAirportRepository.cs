using testeaec.Models;
using testeaec.Responses;
using Weather.Context;

namespace testeaec.Repository
{
    namespace WeatherData
    {
        public class WeatherAirportRepository : IWeatherAirportRepository
        {
            private readonly WeatherContext _context;
            private readonly ILogger<WeatherAirportRepository> _logger;

            public WeatherAirportRepository(WeatherContext context, ILogger<WeatherAirportRepository> logger)
            {
                _context = context;
                _logger = logger;
            }

            public void SaveWeatherAirport(WeatherAirportResponse response)
            {
                try
                {
                    var airport = _context.Airports.FirstOrDefault(a => a.ICAOCode == response.codigo_icao);
                    if (airport == null)
                    {
                        airport = new Airport { ICAOCode = response.codigo_icao };
                        _context.Airports.Add(airport);
                    }

                    airport.Humidity = response.umidade;
                    airport.Visibility = response.visibilidade;
                    airport.AtmosphericPressure = response.pressao_atmosferica;
                    airport.WindSpeed = response.vento;
                    airport.WindDirection = response.direcao_vento;
                    airport.ConditionCode = response.condicao;
                    airport.ConditionDescription = response.condicao_desc;
                    airport.Temperature = response.temp;
                    airport.LastUpdated = response.atualizado_em;

                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving WeatherAirportResponse data");
                }
            }
        }
    }
}