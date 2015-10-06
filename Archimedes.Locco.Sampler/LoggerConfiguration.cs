using System.IO;
using System.Reflection;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Archimedes.Locco.Sampler
{


    public class LoggerConfiguration
    {
        private readonly string _logFolder;
        private readonly string _logFile;


        public LoggerConfiguration(string appDataFolder, string logLevel) :
            this(appDataFolder, LogLevelMap.GetLogLevel(logLevel))
        {

        }

        public LoggerConfiguration(string appDataFolder, Level logLevel)
        {
            _logFolder = appDataFolder + "\\Logs";
            _logFile = _logFolder + "\\events.log";

            Setup(logLevel);
        }

        /// <summary>
        /// Configures the log4net environment
        /// </summary>
        private void Setup(Level logLevel)
        {
            Directory.CreateDirectory(_logFolder);


            var hierarchy = (Hierarchy)LogManager.GetRepository();

            // Log to a file
            var roller = new RollingFileAppender();
            roller.AppendToFile = false;
            roller.File = _logFile;
            roller.Layout = new PatternLayout("%date %type.%method [%-5level] - %message%newline");
            roller.MaxSizeRollBackups = 5;
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            // Log to the visual studio console
            var appender = new TraceAppender();
            appender.ActivateOptions();
            appender.Layout = new PatternLayout("%method (%line) %date [%-5level] - %message%newline");
            hierarchy.Root.AddAppender(appender);

            hierarchy.Root.Level = logLevel;
            hierarchy.Configured = true;
        }
    }

    public static class LogLevelMap
    {
        static readonly LevelMap LevelMap = new LevelMap();

        static LogLevelMap()
        {
            foreach (FieldInfo fieldInfo in typeof(Level).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (fieldInfo.FieldType == typeof(Level))
                {
                    LevelMap.Add((Level)fieldInfo.GetValue(null));
                }
            }
        }

        public static Level GetLogLevel(string logLevel)
        {
            if (string.IsNullOrWhiteSpace(logLevel))
            {
                return null;
            }
            else
            {
                return LevelMap[logLevel];
            }
        }
    }

}
