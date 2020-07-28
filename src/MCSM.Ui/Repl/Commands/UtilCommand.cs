using System;
using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
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
        public ExitCommand() : base("exit", "Exits mcsm")
        {
            Handler = CommandHandler.Create(Execute);
        }

        public void Execute()
        {
        }
    }
}