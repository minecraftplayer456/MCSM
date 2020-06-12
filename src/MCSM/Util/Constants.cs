using Serilog.Events;

namespace MCSM.Util
{
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
        public const string MCSMShortNameVersion = "MCMS - " + MCSMVersion;
    }
}