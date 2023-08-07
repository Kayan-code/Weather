using System.Net.Http;
using testeaec.HttpData;
using testeaec.Repository;
using testeaec.Repository.WeatherData;
using testeaec.Responses;
using Newtonsoft.Json;
using System.Diagnostics;
using Weather.Repository;
using Weather.Responses;

namespace testeaec.Services;

public class WeatherAirportService : IWeatherAirportService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private readonly ILogger<WeatherAirportService> _logger;
    private readonly ILogRepository _logRepository;
    private readonly IWeatherAirportRepository _weatherAirportRepository;


    public WeatherAirportService(IWeatherAirportRepository weatherAirportRepository,
        ILogger<WeatherAirportService> logger, ILogRepository logRepository)
    {
        _logger = logger;
        _weatherAirportRepository = weatherAirportRepository;
        _logRepository = logRepository;
    }

    public async Task<WeatherAirportResult> GetAirportWeatherAsync(WeatherAirportRequest request)
    {
        bool success = false;
        WeatherAirportResult result = new WeatherAirportResult();
        WeatherAirportResponse resp = new WeatherAirportResponse();
        string apiUrl = $"https://brasilapi.com.br/api/cptec/v1/clima/aeroporto/{request.CodICAO}";
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            // Faz a requisição GET
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            success = response.IsSuccessStatusCode;
            // Verifica se a resposta foi bem-sucedida
            if (success)
            {
                // Lê o conteúdo da resposta como string
                string conteudoResposta = await response.Content.ReadAsStringAsync();

                // Desserializa o JSON para o objeto WeatherAirportResponse
                resp = JsonConvert.DeserializeObject<WeatherAirportResponse>(conteudoResposta);

                result.data = resp;

                // Exibe todos os campos no console
                Console.WriteLine($"Umidade: {resp.umidade}");
                Console.WriteLine($"Visibilidade: {resp.visibilidade}");
                Console.WriteLine($"Código ICAO: {resp.codigo_icao}");
                Console.WriteLine($"Pressão Atmosférica: {resp.pressao_atmosferica}");
                Console.WriteLine($"Vento: {resp.vento}");
                Console.WriteLine($"Direção do Vento: {resp.direcao_vento}");
                Console.WriteLine($"Condição do Tempo: {resp.condicao}");
                Console.WriteLine($"Descrição da Condição do Tempo: {resp.condicao_desc}");
                Console.WriteLine($"Temperatura: {resp.temp}");
                Console.WriteLine($"Data e Hora da Última Atualização: {resp.atualizado_em}");

                _weatherAirportRepository.SaveWeatherAirport(resp);


            }
            else
            {
                Console.WriteLine($"Falha na requisição. Status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro na requisição: {ex.Message}");
            _logger.LogError(ex, $"Erro na requisição: {ex.Message}");
        }
        finally
        {
            stopwatch.Stop();
            result.log.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            result.log.QueryId = Guid.NewGuid().ToString();
            result.log.QueryDate = DateTime.Now;
            result.log.ErrorMessage = String.IsNullOrEmpty(resp.codigo_icao) ? MessagesResponse.NotFound : "";

            if (!success)
                _logRepository.SaveLogError(result.log);
        }

        return result;
    }
}
