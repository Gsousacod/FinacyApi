using Microsoft.Extensions.Logging;

namespace FinacyApi.Services
{
   using Microsoft.Extensions.Logging;

public class LoggingService
{
    private readonly ILogger<LoggingService> _logger;

    public LoggingService(ILogger<LoggingService> logger)
    {
        _logger = logger;
    }

   
    public void LogLoginAttempt(string username, bool success)
    {
        if (success)
        {
            _logger.LogInformation("Usuário '{Username}' fez login com sucesso às {Time}.", username, DateTime.UtcNow);
        }
        else
        {
            _logger.LogWarning("Tentativa de login falhou para '{Username}' às {Time}.", username, DateTime.UtcNow);
        }
    }

    
   
    public void LogWarning(string message)
    {
        _logger.LogWarning("[WARN] {Timestamp} - {Message}", DateTime.UtcNow, message);
    }

  
    public void LogError(int errorCode, string message, Exception? ex = null, Dictionary<string, object>? details = null)
    {
        var detailsMessage = details != null
            ? string.Join("; ", details.Select(d => $"{d.Key}: {d.Value}"))
            : "Sem detalhes adicionais.";

        if (ex != null)
        {
            _logger.LogError(ex, "Erro {ErrorCode}: {Message} | Detalhes: {Details}", errorCode, message, detailsMessage);
        }
        else
        {
            _logger.LogError("Erro {ErrorCode}: {Message} | Detalhes: {Details}", errorCode, message, detailsMessage);
        }
    }
}

}