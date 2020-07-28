using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Threading;
using MCSM.Api.Ui;
using Serilog;
using RootCommand = MCSM.Ui.Repl.Commands.RootCommand;

namespace MCSM.Ui.Repl
{
    public class Repl : IRepl
    {
        private readonly ILogger _log;
        private readonly Parser _parser;
        private readonly Thread _replThread;

        public Repl(Command command)
        {
            _log = Log.ForContext<Repl>();
            _replThread = new Thread(Execute);
            _parser = new CommandLineBuilder(command)
                .UseVersionOption()
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

        public Repl() : this(new RootCommand())
        {
        }

        public bool Running { get; private set; }

        public void Run()
        {
            _log.Debug("Run repl");
            _replThread.Start();
        }

        public ParseResult ComputeInput(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            var parseResult = _parser.Parse(input);
            parseResult.InvokeAsync();
            return parseResult;
        }

        public void Stop()
        {
            _log.Debug("Stopping repl");
            Running = false;
            if (!_replThread.Join(5000))
                _log.Warning("Repl thread is still alive. Programme was not exited by repl command!");
        }

        private void Execute()
        {
            Running = true;
            while (Running)
            {
                Console.Write(">>>");
                var input = Console.ReadLine();
                ComputeInput(input);
                if (input != null) Console.WriteLine();
            }
        }
    }
}