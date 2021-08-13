using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api.Services.Logger
{
    public class ActivityLogger : IActivityLogger
    {
        private readonly ILogger _logger;
        public ActivityLogger(ILogger<ActivityLogger> logger)
        {
            _logger = logger;
        }
        public void LogError(Exception ex)
        {
            _logger.LogError($"Error Message :{ex.Message} \n StackTrace: { ex.StackTrace} \n InnerException: {ex.InnerException}");
        }

        public void LogError(string errorMessage)
        {
            _logger.LogError($"{errorMessage}");
        }


        public void LogMessage(string message)
        {
            _logger.LogInformation($"{message}");
        }
    }
}
