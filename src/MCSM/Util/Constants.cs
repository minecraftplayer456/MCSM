using Serilog.Events;

namespace MCSM.Util
{
    /// <summary>
    /// Utils class with global constants
    /// Some of them depends on the run configuration(Debug, Release)
    /// </summary>
    public static class Constants
    {
        #region DependOnConfiguration
#if DEBUG
        public const LogEventLevel DefaultLogLevel = LogEventLevel.Verbose;
#else
        public const LogEventLevel DefaultLogLevel = LogEventLevel.Information;
#endif
        #endregion

        public const string MCSMVersion = "Alpha-0.1.0";
    }
}