using System.IO.Abstractions;
using MCSM.Core.Manager.IO;
using MCSM.Core.Util;
using MCSM.Ui.Cli;
using MCSM.Ui.Repl;
using Serilog;
using Serilog.Events;

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
        ///     Returns current IFileManager
        /// </summary>
        public IFileManager FileManager { get; }

        /// <summary>
        ///     Current repl instance
        /// </summary>
        public IRepl Repl { get; }

        /// <summary>
        ///     Starts the application
        /// </summary>
        void Start(string[] args);

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
            FileManager = new FileManager(new FileSystem());
            Repl = new Repl();

            _log = Log.ForContext<Application>();
        }

        public ILogManager LogManager { get; }
        public IFileManager FileManager { get; }
        public IRepl Repl { get; }

        public void Start(string[] args)
        {
            _log.Information("Starting MCSM - v.{version}", Constants.McsmVersion);

            var cli = new Cli();

            Configure(cli.Parse(args));

            Repl.Run();
        }

        public void Stop()
        {
            _log.Information("Stopping MCSM");

            Repl.Stop();
        }

        private void Configure(CliResult result)
        {
            if (result.Debug) LogManager.LogLevel = LogEventLevel.Verbose;
        }
    }
}