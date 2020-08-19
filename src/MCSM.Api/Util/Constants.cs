using System.Reflection;
using Serilog.Events;

namespace MCSM.Api.Util
{
    /// <summary>
    ///     Class to hold constants. Some of them depends on configuration
    /// </summary>
    public static class Constants
    {
        /// <summary>
        ///     Current Mcsm version from assembly information
        /// </summary>
        public static readonly string McsmVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

        #region ConfigurationConstants

#if DEBUG
        /// <summary>
        ///     Default log level. Can be changed by cli
        /// </summary>
        public static readonly LogEventLevel LogLevel = LogEventLevel.Verbose;
#else
        public static readonly LogEventLevel LogLevel = LogEventLevel.Information;
#endif

        #endregion
    }
}