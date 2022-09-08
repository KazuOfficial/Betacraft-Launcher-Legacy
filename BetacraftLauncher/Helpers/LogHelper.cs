using Caliburn.Micro;
using Serilog;
using System;

namespace BetacraftLauncher.Helpers
{
    public class LogHelper : ILog
    {
        private string launcherPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + @"\.betacraftlegacy\";

        #region Fields

        #endregion

        private readonly ILogger _logger;
        #region Constructors
        public LogHelper()
        {
            _logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File(launcherPath + "launcher.log").CreateLogger();
        }
        #endregion

        #region Helper Methods
        private string CreateLogMessage(string format, params object[] args)
        {
            return string.Format("[{0}] {1}",
                DateTime.Now.ToString("o"),
                string.Format(format, args));
        }
        #endregion

        #region ILog Members
        public void Error(Exception exception)
        {
            _logger.Error(CreateLogMessage(exception.ToString()), "ERROR");
        }

        public void Info(string format, params object[] args)
        {
            if (Array.FindIndex(args, t => t.ToString().Contains("Something i dont want to log")) >= 0)
                return;
            _logger.Information(CreateLogMessage(format, args), "INFO");
        }

        public void Warn(string format, params object[] args)
        {
            _logger.Warning(CreateLogMessage(format, args), "WARN");
        }
        #endregion
    }
}
