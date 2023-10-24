using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.Interfaces
{
    /// <summary>
    /// The logger manager that contains methods for loggings our messages
    /// </summary>
    public interface ILoggerManager
    {
        /// <summary>
        /// function for logging Information messages
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);
        /// <summary>
        /// function for warning messages
        /// </summary>
        /// <param name="message"></param>
        void LogWarn(string message);
        /// <summary>
        /// function for debugging messages
        /// </summary>
        /// <param name="message"></param>
        void LogDebug(string message);
        /// <summary>
        /// function for logging error messages
        /// </summary>
        /// <param name="message"></param>
        void LogError(string message);

    }
}
