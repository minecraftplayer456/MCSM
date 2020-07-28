using System.Reflection;
using Serilog.Events;

namespace MCSM.Api.Util
{
    /// <summary>
    ///     Class to hold constants. Some of them depends on configuration
    /// </summary>
    public static class Constants
    {
        public static readonly string McsmVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

        #region ConfigurationConstants

#if DEBUG
        public static readonly LogEventLevel LogLevel = LogEventLevel.Verbose;
#else
        public static readonly LogEventLevel LogLevel = LogEventLevel.Information;
#endif

        #endregion
    }
}