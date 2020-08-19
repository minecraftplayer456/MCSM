using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using MCSM.Api;
using MCSM.Api.Util;
using IConsole = MCSM.Api.Ui.IConsole;

namespace MCSM.Ui.Repl.Commands
{
    /// <summary>
    ///     This command prints the current assembly version on console
    /// </summary>
    public class VersionCommand : Command
    {
        private readonly IConsole _console;

        public VersionCommand(IConsole console) : base("version", "Displays current version of MCSM")
        {
            _console = console;

            //Bind execute method
            Handler = CommandHandler.Create(Execute);
        }

        public void Execute()
        {
            //Prints current assembly version on command line
            _console.WriteLine(Constants.McsmVersion);
        }
    }

    /// <summary>
    ///     This command prints the help menu
    /// </summary>
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "Shows information about all commands")
        {
            // Add alias h
            AddAlias("h");

            //Bind execution method
            Handler = CommandHandler.Create<InvocationContext>(Execute);
        }

        public void Execute(InvocationContext context)
        {
            //Print help menu on the console
            new HelpBuilder(context.Console).Write(context.BindingContext.ParseResult.RootCommandResult.Command);
        }
    }

    /// <summary>
    ///     This command exits the application soft (saving configs, shutdown servers, ...) or hard
    /// </summary>
    public class ExitCommand : Command
    {
        private readonly IApplicationLifecycle _lifecycle;

        public ExitCommand(IApplicationLifecycle lifecycle) : base("exit", "Exits mcsm")
        {
            _lifecycle = lifecycle;

            //Bind execution method
            Handler = CommandHandler.Create(Execute);
        }

        public void Execute()
        {
            //TODO Add soft and hard stop
            //Stops the application
            _lifecycle.Stop();
        }
    }
}