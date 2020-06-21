using Serilog.Events;

namespace MCSM.Util
{
    /// <summary>
    ///     This class contains all constant values that will never change. Also when the build configuration is changed.
    /// </summary>
    public static class Constants
    {
        public const string DefaultWorkspacePath = "Workspace";
        public const string DefaultWorkspaceFileName = "workspace.json";
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