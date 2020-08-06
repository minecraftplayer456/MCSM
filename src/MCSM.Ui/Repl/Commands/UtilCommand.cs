using System;
using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using MCSM.Api;
using MCSM.Api.Util;
using MCSM.Ui.Util;
using Serilog;
using Console = System.Console;
using IConsole = MCSM.Api.Ui.IConsole;

namespace MCSM.Ui.Repl.Commands
{
    public class VersionCommand : Command
    {
        private readonly IConsole _console;
        
        public VersionCommand(IConsole console) : base("version", "Displays current version of MCSM")
        {
            _console = console;
            
            Handler = CommandHandler.Create(Execute);
        }

        public void Execute()
        {
            _console.WriteLine(Constants.McsmVersion);
        }
    }

    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "Shows information about all commands")
        {
            Handler = CommandHandler.Create<InvocationContext>(Execute);
        }

        public void Execute(InvocationContext context)
        {
            new HelpBuilder(context.Console).Write(context.BindingContext.ParseResult.RootCommandResult.Command);
        }
    }

    public class ExitCommand : Command
    {
        private readonly IApplicationLifecycle _lifecycle;

        public ExitCommand(IApplicationLifecycle lifecycle) : base("exit", "Exits mcsm")
        {
            _lifecycle = lifecycle;

            Handler = CommandHandler.Create(Execute);
        }

        public void Execute()
        {
            _lifecycle.Stop();
        }
    }
}