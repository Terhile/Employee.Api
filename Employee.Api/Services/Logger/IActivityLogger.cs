using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api.Services.Logger
{
   public interface IActivityLogger
    {
        void LogMessage(string message);
        void LogError(Exception ex);
        void LogError(string errorMessage);
    }
}
