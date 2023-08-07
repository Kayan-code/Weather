using testeaec.Models;

namespace Weather.Repository
{
    public interface ILogRepository
    {
        void SaveLogError(LogResponse response);
    }
}
