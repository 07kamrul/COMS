using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class COMSLogger : ICOMSLogger
    {
        static Serilog.ILogger _logger;
        static string _logLevel = "Information";
        static string _filePath = Path.Combine(Environment.CurrentDirectory, "logs", "app_log.log");

        public COMSLogger() : this(_filePath, _logLevel)
        {

        }

        public COMSLogger(string filpPath) : this(filpPath, _logLevel)
        {

        }

        public COMSLogger(string filpPath, string logLevel)
        {
            if(_logger == null)
            {
                _logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.Console().WriteTo
                    .File(
                    path: filpPath,
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 1048760,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 3
                    ).CreateLogger();
            }
        }

        public void Information(string logText)
        {
            _logger.Information(logText);
        }

        public void Warning(string logText)
        {
            _logger.Warning(logText);
        }
        public void Error(string logText)
        {
            _logger.Error(logText);
        }

        public void Debug(string logText)
        {
            _logger.Debug(logText);
        }

        public void Fatal(string logText)
        {
            _logger.Fatal(logText);            
        }
    }

    public class NullLogger : ICOMSLogger
    {
        public void Debug(string logText)
        {
            
        }

        public void Error(string logText)
        {
            
        }

        public void Fatal(string logText)
        {
            
        }

        public void Information(string logText)
        {
            
        }

        public void Warning(string logText)
        {
            
        }
    }
}
