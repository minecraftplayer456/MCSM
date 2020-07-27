using Serilog;
using Serilog.Core;
using Serilog.Events;
using Constants = MCSM.Core.Util.Constants;

namespace MCSM.Core.Manager.IO
{
    /// <summary>
    ///     Manager for logger initialization
    /// </summary>
    public interface ILogManager
    {
        /// <summary>
        ///     Current root log event
        /// </summary>
        public LogEventLevel LogLevel { get; set; }
    }

    public class LogManager : ILogManager
    {
        private readonly LoggingLevelSwitch _levelSwitch;

        public LogManager(LogEventLevel logLevel)
        {
            _levelSwitch = new LoggingLevelSwitch(logLevel);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(_levelSwitch)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "{Timestamp:HH:mm:ss.fff} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}")
                .WriteTo.File("logs\\latest.txt",
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] ({SourceContext:1}) {Message}{NewLine}{Exception}")
                .CreateLogger();
        }

        public LogManager() : this(Constants.LogLevel)
        {
        }

        public LogEventLevel LogLevel
        {
            get => _levelSwitch.MinimumLevel;
            set => _levelSwitch.MinimumLevel = value;
        }
    }
}