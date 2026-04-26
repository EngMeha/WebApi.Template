namespace WebApi.Template.Application.Interfaces;

public interface ILoggerService
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message, Exception? e);
    void LogError(string message);
}