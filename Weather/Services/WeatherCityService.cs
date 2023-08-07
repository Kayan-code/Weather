using Azure;
using Newtonsoft.Json;
using System.Diagnostics;
using testeaec.HttpData;
using testeaec.Models;
using testeaec.Repository;
using testeaec.Responses;
using testeaec.Services;
using Weather.Repository;
using Weather.Responses;

namespace Weather.Services
{
    public class WeatherCityService : IWeatherCityService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly ILogger<WeatherCityService> _logger;
        private readonly ILogRepository _logRepository;
        private readonly IWeatherCityRepository _weatherCityRepository;


        public WeatherCityService(IWeatherCityRepository weatherCityRepository,
            ILogger<WeatherCityService> logger,
            ILogRepository logRepository)
        {
            _logger = logger;
            _weatherCityRepository = weatherCityRepository;
            _logRepository = logRepository;
        }

        public async Task<List<WeatherCityResult>> GetCityByCityNameWeatherAsync(WeatherCityRequest request)
        {
            bool success = false;
            List<WeatherCityResult> result = new List<WeatherCityResult>();
            WeatherCityResponse resp = new WeatherCityResponse();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            if (request.CityCode <= 0)
            {

                var cities = await GetIdCity(request.CityName).ConfigureAwait(false);


                foreach (var city in cities)
                {
                    WeatherCityResult resultItem = new WeatherCityResult();

                    string apiUrl = request.Days > 0 && request.Days <= 6
                        ? $"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{city.id}/{request.Days}"
                        : $"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{city.id}";

                    try
                    {
                        // Faz a requisição GET
                        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                        // Verifica se a resposta foi bem-sucedida
                        if (response.IsSuccessStatusCode)
                        {
                            // Lê o conteúdo da resposta como string
                            string responseContent = await response.Content.ReadAsStringAsync();

                            // Desserializa o JSON para o objeto WeatherCityResponse
                            resp = JsonConvert.DeserializeObject<WeatherCityResponse>(responseContent);
                            resultItem.data = resp;

                            // Exibe todos os campos no console
                            Console.WriteLine($"Cidade: {JsonConvert.SerializeObject(resp.cidade)}");
                            Console.WriteLine($"Estado: {JsonConvert.SerializeObject(resp.estado)}");
                            Console.WriteLine(
                                $"Atualizado em: {JsonConvert.SerializeObject(resp.atualizado_em.ToString("dd/MM/yyyy"))}");
                            Console.WriteLine($"Clima na cidade: {JsonConvert.SerializeObject(resp.clima)}");

                            try
                            {
                                _weatherCityRepository.SaveWeatherCities(resp);
                            }
                            catch (HttpRequestException e)
                            {
                                Console.WriteLine($"Erro na requisição. Status code: {response.StatusCode}");
                                _logger.LogError(e, $"Erro na requisição. Status code:  {response.StatusCode}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Erro na requisição. Status code: {response.StatusCode}");
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Erro na requisição. Status code: {ex.StatusCode}");
                        _logger.LogError(ex, $"Erro na requisição: {ex.Message}");
                    }
                    finally
                    {
                        stopwatch.Stop();
                        resultItem.log.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                        resultItem.log.QueryId = Guid.NewGuid().ToString();
                        resultItem.log.QueryDate = DateTime.Now;
                        resultItem.log.ErrorMessage =
                            String.IsNullOrEmpty(resp.cidade) ? MessagesResponse.NotFound : "";

                        result.Add(resultItem);

                        if (!success)
                            _logRepository.SaveLogError(resultItem.log);
                    }
                }
            }
            else
            {
                WeatherCityResult resultItem = new WeatherCityResult();

                string apiUrl = request.Days > 0 && request.Days <= 6
                    ? $"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{request.CityCode}/{request.Days}"
                    : $"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{request.CityCode}";

                try
                {
                    // Faz a requisição GET
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                    // Verifica se a resposta foi bem-sucedida
                    if (response.IsSuccessStatusCode)
                    {
                        // Lê o conteúdo da resposta como string
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Desserializa o JSON para o objeto WeatherCityResponse
                        resp = JsonConvert.DeserializeObject<WeatherCityResponse>(responseContent);
                        resultItem.data = resp;

                        // Exibe todos os campos no console
                        Console.WriteLine($"Cidade: {JsonConvert.SerializeObject(resp.cidade)}");
                        Console.WriteLine($"Estado: {JsonConvert.SerializeObject(resp.estado)}");
                        Console.WriteLine(
                            $"Atualizado em: {JsonConvert.SerializeObject(resp.atualizado_em.ToString("dd/MM/yyyy"))}");
                        Console.WriteLine($"Clima na cidade: {JsonConvert.SerializeObject(resp.clima)}");

                        try
                        {
                            _weatherCityRepository.SaveWeatherCities(resp);
                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine($"Erro na requisição. Status code: {response.StatusCode}");
                            _logger.LogError(e, $"Erro na requisição. Status code:  {response.StatusCode}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Erro na requisição. Status code: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Erro na requisição. Status code: {ex.StatusCode}");
                    _logger.LogError(ex, $"Erro na requisição: {ex.Message}");
                }
                finally
                {
                    stopwatch.Stop();
                    resultItem.log.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                    resultItem.log.QueryId = Guid.NewGuid().ToString();
                    resultItem.log.QueryDate = DateTime.Now;
                    resultItem.log.ErrorMessage =
                        String.IsNullOrEmpty(resp.cidade) ? MessagesResponse.NotFound : "";

                    result.Add(resultItem);

                    if (!success)
                        _logRepository.SaveLogError(resultItem.log);
                }
            }

            return result;
        }

        public async Task<WeatherCityResult> GetCityByIdCityWeatherAsync(WeatherCityRequest request)
        {
            bool success = false;
            WeatherCityResult result = new WeatherCityResult();
            WeatherCityResponse resp = new WeatherCityResponse();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string apiUrl = request.Days > 0 && request.Days <= 6 ?
                $"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{request.CityCode}/{request.Days}" :
                $"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{request.CityCode}";

            try
            {
                // Faz a requisição GET
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                // Verifica se a resposta foi bem-sucedida
                if (response.IsSuccessStatusCode)
                {
                    // Lê o conteúdo da resposta como string
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Desserializa o JSON para o objeto WeatherCityResponse
                    resp = JsonConvert.DeserializeObject<WeatherCityResponse>(responseContent);

                    result.data = resp;

                    // Exibe todos os campos no console
                    Console.WriteLine($"Cidade: {JsonConvert.SerializeObject(resp.cidade)}");
                    Console.WriteLine($"Estado: {JsonConvert.SerializeObject(resp.estado)}");
                    Console.WriteLine($"Atualizado em: {JsonConvert.SerializeObject(resp.atualizado_em.ToString("dd/MM/yyyy"))}");
                    Console.WriteLine($"Clima na cidade: {JsonConvert.SerializeObject(resp.clima)}");

                    try
                    {
                        _weatherCityRepository.SaveWeatherCities(resp);
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Erro na requisição. Status code: {response.StatusCode}");
                        _logger.LogError(e, $"Erro na requisição. Status code:  {response.StatusCode}");
                    }
                }
                else
                {
                    Console.WriteLine($"Erro na requisição. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro na requisição. Status code: {ex.StatusCode}");
                _logger.LogError(ex, $"Erro na requisição: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();
                result.log.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                result.log.QueryId = Guid.NewGuid().ToString();
                result.log.QueryDate = DateTime.Now;
                result.log.ErrorMessage = String.IsNullOrEmpty(resp.cidade) ? MessagesResponse.NotFound : "";

                if (!success)
                    _logRepository.SaveLogError(result.log);
            }

            return result;
        }

        public async Task<List<CityBody>> GetIdCity(string cityName)
        {

            string apiUrl = $"https://brasilapi.com.br/api/cptec/v1/cidade/{cityName}";

            // Faz a requisição GET
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl).ConfigureAwait(false);


            // Lê o conteúdo da resposta como string
            string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            var resp = JsonConvert.DeserializeObject<List<CityBody>>(responseContent);

            return resp;

        }

        public class CityBody
        {
            public int id { get; set; }
            public string nome { get; set; }
            public string estado { get; set; }
        }
    }
}
