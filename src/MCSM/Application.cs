using System.IO.Abstractions;
using System.Threading.Tasks;
using MCSM.Api;
using MCSM.Api.Manager.IO;
using MCSM.Api.Ui;
using MCSM.Api.Util;
using MCSM.Core.Manager.IO;
using MCSM.Ui.Cli;
using MCSM.Ui.Repl;
using MCSM.Ui.Util;
using Serilog;
using Serilog.Events;

namespace MCSM
{
    public class Application : IApplication
    {
        private readonly ILogger _log;

        /// <summary>
        ///     Creates new application instance that can run isolated from other application instances
        /// </summary>
        public Application()
        {
            //Initializes programme components and inject them each other
            LogManager = new LogManager();
            FileManager = new FileManager(new FileSystem());
            Console = new Console();
            Repl = new Repl(this);
            _log = Log.ForContext<Application>();
        }

        public ILogManager LogManager { get; }
        public IFileManager FileManager { get; }
        public IRepl Repl { get; }
        public IConsole Console { get; }

        public async Task Start(string[] args)
        {
            _log.Information("Starting MCSM - v.{version}", Constants.McsmVersion);

            //Parse command line arguments
            var cli = new Cli();
            var cliResult = cli.Parse(args);

            //Sets options from result of command line parsing
            if (cliResult.Debug) LogManager.RootLogLevel = LogEventLevel.Verbose;

            //Run the repl after initialization
            if (!cliResult.NoRepl) await Repl.Run();
        }

        public void Stop()
        {
            _log.Information("Stopping MCSM");

            //Stops the repl loop
            Repl.Exit();
        }
    }
}