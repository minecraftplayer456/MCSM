using MCSM.Core.Util;
using Serilog;
using Serilog.Events;

namespace MCSM.Core.Manager.IO
{
    /// <summary>
    ///     Manager for logger initialization
    /// </summary>
    public interface ILogManager
    {
    }

    public class LogManager : ILogManager
    {
        public LogManager(LogEventLevel logLevel)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(logLevel)
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
    }
}