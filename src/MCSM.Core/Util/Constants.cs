using Serilog.Events;

namespace MCSM.Core.Util
{
    public static class Constants
    {
        public static readonly string McsmVersion = "Alpha-0.1.0";

        #region ConfigurationConstants

#if DEBUG
        public static readonly LogEventLevel LogLevel = LogEventLevel.Verbose;
#else
        public static readonly LogEventLevel LogLevel = LogEventLevel.Information;
#endif

        #endregion
    }
}