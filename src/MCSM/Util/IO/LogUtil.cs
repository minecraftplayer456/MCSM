using Serilog;
using Serilog.Core;

namespace MCSM.Util.IO
{
    /// <summary>
    ///     Utility class for managing Serilog logger globally
    /// </summary>
    public static class LogUtil
    {
        private static bool _initialized;
        private static LoggingLevelSwitch _consoleSwitch;

        /// <summary>
        ///     Initialize the logger with verbose (debug configuration) or information (release configuration). Will only execute
        ///     one time
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;

            _consoleSwitch = new LoggingLevelSwitch();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(ConfigurationConstants.DefautLogLevel)
                .WriteTo.Console().MinimumLevel.ControlledBy(_consoleSwitch)
                .WriteTo.File("logs\\latest.txt")
                .CreateLogger();

            _initialized = true;
        }

        /// <summary>
        ///     Logging mode to ui. Log level = fatal
        /// </summary>
        public static void SwitchToUi()
        {
            _consoleSwitch.MinimumLevel = Constants.DefaultUiConsoleLogLevel;
        }

        /// <summary>
        ///     Logging mode to console. Log level = information
        /// </summary>
        public static void SwitchToConsole()
        {
            _consoleSwitch.MinimumLevel = ConfigurationConstants.DefautLogLevel;
        }
    }
}