namespace testeaec.Models;

public class LogResponse
{
    /// <summary>
    ///     Unique id table
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     The number of milliseconds elapsed during the call
    /// </summary>
    public decimal ElapsedMilliseconds { get; set; }

    /// <summary>
    ///     The date when the request was received and processed
    /// </summary>
    public DateTime QueryDate { get; set; }

    /// <summary>
    ///     Unique id caller
    /// </summary>
    public string QueryId { get; set; }

    /// <summary>
    ///     Message exception
    /// </summary>
    public string ErrorMessage { get; set; }
}