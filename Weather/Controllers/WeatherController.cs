using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using testeaec.HttpData;
using testeaec.Models;
using testeaec.Services;
using Weather.Responses;
using Weather.Services;

namespace testeaec.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherAirportService _weatherAirportService;
    private readonly IWeatherCityService _weatherCityService;

    public WeatherController(IWeatherAirportService weatherAirportService, IWeatherCityService weatherCityService)
    {
        _weatherAirportService = weatherAirportService;
        _weatherCityService = weatherCityService;
    }

    /// <summary>
    ///     Returning weather in the city by city name
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("city")]
    public async Task<ActionResult<List<WeatherCityResult>>> WeatherInCityByName(WeatherCityRequest request)
    {
        List<WeatherCityResult> result = new List<WeatherCityResult>();

        if ((request.Days <= 0 || request.Days > 6))
        {
            result.Add(new WeatherCityResult()
            {
                log = new LogResponse()
                {
                    ErrorMessage = MessagesResponse.DaysMinAndMax
                }
            });

            return BadRequest(result);
        }

        var ret = await _weatherCityService.GetCityByCityNameWeatherAsync(request).ConfigureAwait(false);

        return Ok(ret);
    }


    /// <summary>
    ///     Returning weather in the city by city id
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("city")]
    public async Task<ActionResult<WeatherCityResult>> WeatherInCityByCod(int codcity, int days)
    {
        WeatherCityResult result = new WeatherCityResult();

        if ((days <= 0 || days > 6))
        {
            result.log.ErrorMessage = MessagesResponse.DaysMinAndMax;
            return BadRequest(result);
        }

        WeatherCityRequest request = new WeatherCityRequest()
        {
            Days = days,
            CityCode = codcity,
        };

        var ret = await _weatherCityService.GetCityByIdCityWeatherAsync(request).ConfigureAwait(false);

        return Ok(ret);
    }


    /// <summary>
    ///     Returning weather in the airport
    /// </summary>
    /// <param name="codIcao"></param>
    /// <returns></returns>
    [HttpGet("airport/{codIcao}")]
    public async Task<ActionResult<WeatherAirportResult>> WeatherInAirport(string codIcao)
    {
        var request = new WeatherAirportRequest() { CodICAO = codIcao };

        var ret = await _weatherAirportService.GetAirportWeatherAsync(request).ConfigureAwait(false);

        return Ok(ret);
    }
}