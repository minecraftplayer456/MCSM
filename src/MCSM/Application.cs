using System.IO.Abstractions;
using MCSM.Api;
using MCSM.Api.Manager.IO;
using MCSM.Api.Ui;
using MCSM.Api.Util;
using MCSM.Core.Manager.IO;
using MCSM.Ui.Cli;
using MCSM.Ui.Repl;
using Serilog;
using Serilog.Events;

namespace MCSM
{
    public class Application : IApplication
    {
        private readonly ILogger _log;

        public Application()
        {
            LogManager = new LogManager();
            FileManager = new FileManager(new FileSystem());
            Repl = new Repl(this);

            _log = Log.ForContext<Application>();
        }

        public ILogManager LogManager { get; }
        public IFileManager FileManager { get; }
        public IRepl Repl { get; }

        public void Start(string[] args)
        {
            _log.Information("Starting MCSM - v.{version}", Constants.McsmVersion);

            var cli = new Cli();

            var cliResult = cli.Parse(args);
            
            if (cliResult.Debug) LogManager.RootLogLevel = LogEventLevel.Verbose;

            if(!cliResult.NoRepl) Repl.Run();
        }

        public void Stop()
        {
            _log.Information("Stopping MCSM");

            Repl.Exit();
        }
    }
}