using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Real_Time_Chat_Application.Models
{
    public interface IActivityLogger
    {
        void LogActivity(string description, string level, string createdBy, string exception = null);
    }

    public class ActivityLogger : IActivityLogger
    {
        private readonly ILogger<ActivityLogger> _logger;

        public ActivityLogger(ILogger<ActivityLogger> logger)
        {
            _logger = logger;
        }

        public void LogActivity(string description, string level, string createdBy, string exception = null)
        {
            var logLevel = level switch
            {
                "Information" => LogLevel.Information,
                "Warning" => LogLevel.Warning,
                "Error" => LogLevel.Error,
                _ => LogLevel.Information
            };

            // Log using structured logging
            _logger.Log(logLevel,
                "{Description} | Created_By: {CreatedBy} | Exception: {Exception}",
                description,
                createdBy,
                exception);
        }
    }
}
