using MCSM.Core.Manager;
using MCSM.Core.Util;
using Serilog;

namespace MCSM
{
    /// <summary>
    ///     Application class which starts, controls and stops the application. Contains all managers and give them their
    ///     dependencies.
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        ///     Starts the application
        /// </summary>
        void Start();

        /// <summary>
        ///     Stops the application
        /// </summary>
        void Stop();
    }

    public class Application : IApplication
    {
        private readonly ILogger _log;
        public readonly ILogManager LogManager;

        public Application()
        {
            LogManager = new LogManager();
            _log = Log.ForContext<Application>();
        }

        public void Start()
        {
            _log.Information("Starting MCSM - v.{version}", Constants.MCSMVersion);
        }

        public void Stop()
        {
            _log.Information("Stopping MCSM");
        }
    }
}