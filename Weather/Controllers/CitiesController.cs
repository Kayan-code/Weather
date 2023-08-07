using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weather.Context;
using testeaec.Models;
using Azure.Core;
using Weather.Services;

namespace Weather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly WeatherContext _context;
        private readonly IWeatherCityService _weatherCityService;

        public CitiesController(WeatherContext context, IWeatherCityService weatherCityService)
        {
            _context = context;
            _weatherCityService = weatherCityService;
        }


        /// <summary>
        ///     Returning weather in the city by city id
        /// </summary>
        /// <param name="cityname"></param>
        /// <returns></returns>
        [HttpGet("{cityname}")]
        public async Task<ActionResult<List<WeatherCityService.CityBody>>> GetIdCity(string cityname)
        {
            var ret = await _weatherCityService.GetIdCity(cityname).ConfigureAwait(false);

            return Ok(ret);
        }
    }
}
