using IdentityService.BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.Logger
{
    /// <summary>
    /// the logger manager class
    /// </summary>
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// initilization of a new instance of <see cref="LoggerManager"/>
        /// </summary>
        public LoggerManager()
        {
            
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        public void LogDebug(string message) => logger.Debug(message);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message) => logger.Error(message);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message) => logger.Info(message);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        public void LogWarn(string message) => logger.Warn(message); 

    }
}
