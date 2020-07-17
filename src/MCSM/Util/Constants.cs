using Serilog.Events;

namespace MCSM.Util
{
    /// <summary>
    ///     This class contains all constant values that will never change. Also when the build configuration is changed.
    /// </summary>
    public static class Constants
    {
        public const string ProgrammeVersion = "Alpha-0.1.0";

        public const string DefaultWorkspacePath = ".\\Workspace";
        public const string DefaultWorkspaceName = "DefaultWorkspace";

        public const LogEventLevel DefaultUiConsoleLogLevel = LogEventLevel.Fatal;
    }

    /// <summary>
    ///     This class contains all constant values that will change if you change the build configuration.
    /// </summary>
    public static class ConfigurationConstants
    {
#if DEBUG

        public const LogEventLevel DefautLogLevel = LogEventLevel.Verbose;

#else
        public const LogEventLevel DefautLogLevel = LogEventLevel.Information;

#endif
    }
}