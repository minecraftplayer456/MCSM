using Serilog;

namespace MCSM.Util
{
    /// <summary>
    ///     Utility class for managing Serilog logger globally
    /// </summary>
    public class LogUtil
    {
        private static readonly bool _initialized = false;

        /// <summary>
        ///     Initialize the logger with verbose (debug configuration) or information (release configuration). Will only execute
        ///     one time
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(ConfigurationConstants.DefautLogLevel)
                .WriteTo.Console()
                .WriteTo.File("logs/latest.txt")
                .CreateLogger();
        }
    }
}