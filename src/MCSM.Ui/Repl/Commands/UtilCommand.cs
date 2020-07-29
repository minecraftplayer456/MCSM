using System;
using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using MCSM.Api;
using MCSM.Api.Util;

namespace MCSM.Ui.Repl.Commands
{
    public class VersionCommand : Command
    {
        public VersionCommand() : base("version", "Displays current version of MCSM")
        {
            Handler = CommandHandler.Create(Execute);
        }

        public void Execute()
        {
            Console.WriteLine(Constants.McsmVersion);
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
        private readonly IApplication _application;

        public ExitCommand(IApplication application) : base("exit", "Exits mcsm")
        {
            _application = application;

            Handler = CommandHandler.Create(Execute);
        }

        public void Execute()
        {
            _application.Stop();
        }
    }
}