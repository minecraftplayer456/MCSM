using Serilog.Events;

namespace MCSM.Core.Util
{
    public static class Constants
    {
        public const string McsmVersion = "Alpha-0.1.0";

        #region ConfigurationConstants

#if DEBUG
        public const LogEventLevel LogLevel = LogEventLevel.Verbose;
#else
        public const LogEventLevel LogLevel = LogEventLevel.Information;
#endif

        #endregion
    }
}