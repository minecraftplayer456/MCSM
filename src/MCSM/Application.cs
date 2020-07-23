using MCSM.Core.Manager.IO;
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
        ///     Returns current ILogManager
        /// </summary>
        public ILogManager LogManager { get; }

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

        public Application()
        {
            LogManager = new LogManager();
            _log = Log.ForContext<Application>();
        }

        public ILogManager LogManager { get; }

        public void Start()
        {
            _log.Information("Starting MCSM - v.{version}", Constants.McsmVersion);
        }

        public void Stop()
        {
            _log.Information("Stopping MCSM");
        }
    }
}