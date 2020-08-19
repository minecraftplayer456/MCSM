using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Threading.Tasks;
using MCSM.Api;
using MCSM.Api.Ui;
using MCSM.Ui.Util;
using Serilog;
using IConsole = MCSM.Api.Ui.IConsole;
using RootCommand = MCSM.Ui.Repl.Commands.RootCommand;

namespace MCSM.Ui.Repl
{
    public class Repl : IRepl
    {
        private readonly IConsole _console;
        private readonly ILogger _log;
        private readonly Parser _parser;

        public Repl(IConsole console, Command command)
        {
            _log = Log.ForContext<Repl>();
            _console = console;
            _parser = new CommandLineBuilder(command)
                .UseHelp()
                .UseEnvironmentVariableDirective()
                .UseParseDirective()
                .UseDebugDirective()
                .UseSuggestDirective()
                .RegisterWithDotnetSuggest()
                .UseTypoCorrections()
                .UseExceptionHandler()
                .CancelOnProcessTermination()
                .Build();
        }

        public Repl(IApplication application) : this(application.Console, new RootCommand(application))
        {
        }

        public bool Running { get; private set; }

        public async Task Run()
        {
            _log.Debug("Run repl");
            Running = true;
            while (Running)
            {
                _console.Write(">>>");
                var input = _console.ReadLine();
                await ComputeInput(input);
                if (input != null) _console.WriteLine();
            }
        }

        public async Task<ParseResult> ComputeInput(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            var parseResult = _parser.Parse(input);
            await parseResult.InvokeAsync(new CommandLineConsole(_console));
            return parseResult;
        }

        public void Exit()
        {
            _log.Debug("Exiting repl");
            Running = false;
        }
    }
}