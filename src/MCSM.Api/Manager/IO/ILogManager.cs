using Serilog.Events;

namespace MCSM.Api.Manager.IO
{
    /// <summary>
    ///     Interface to log options in serilog
    /// </summary>
    public interface ILogManager
    {
        /// <summary>
        ///     Current root log level
        /// </summary>
        LogEventLevel RootLogLevel { get; set; }
    }
}