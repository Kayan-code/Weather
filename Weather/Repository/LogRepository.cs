using Weather.Context;
using testeaec.Models;
using testeaec.Responses;
using Weather.Repository;

namespace testeaec.Repository;

public class LogRepository : ILogRepository
{
    private readonly WeatherContext _context;
    private readonly ILogger<LogRepository> _logger;

    public LogRepository(WeatherContext context, ILogger<LogRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void SaveLogError(LogResponse response)
    {
        try
        {
            _context.LogEntries.Add(response);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving LogResponse data");
        }
    }
}