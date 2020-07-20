using Serilog.Events;

namespace MCSM.Core.Util
{
    public class Constants
    {
        public const string MCSMVersion = "Alpha-0.1.0";

        #region ConfigurationConstants

#if DEBUG
        public const LogEventLevel LogLevel = LogEventLevel.Verbose;
#else
        public const LogEventLevel logLevel = LogEventLevel.Information;
#endif

        #endregion
    }
}