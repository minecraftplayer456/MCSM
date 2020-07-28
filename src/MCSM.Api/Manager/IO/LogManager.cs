using Serilog.Events;

namespace MCSM.Api.Manager.IO
{
    public interface ILogManager
    {
        /// <summary>
        ///     Current root log level
        /// </summary>
        LogEventLevel RootLogLevel { get; set; }
    }
}