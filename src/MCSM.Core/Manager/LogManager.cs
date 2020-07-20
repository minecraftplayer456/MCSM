using MCSM.Core.Util;
using Serilog;

namespace MCSM.Core.Manager
{
    /// <summary>
    ///     Manager for logger initialization
    /// </summary>
    public interface ILogManager
    {
    }

    public class LogManager : ILogManager
    {
        public LogManager()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(Constants.LogLevel)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "{Timestamp:HH:mm:ss.fff} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}")
                .WriteTo.File("logs\\latest.txt",
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] ({SourceContext:1}) {Message}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}