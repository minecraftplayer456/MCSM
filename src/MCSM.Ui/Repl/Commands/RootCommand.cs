using MCSM.Api;
using MCSM.Api.Ui;

namespace MCSM.Ui.Repl.Commands
{
    /// <summary>
    ///     Root command for repl with subcommands
    /// </summary>
    public class RootCommand : System.CommandLine.RootCommand
    {
        public RootCommand(IApplicationLifecycle lifecycle, IConsole console)
        {
            //Add subcommands to root command
            AddCommand(new VersionCommand(console));
            AddCommand(new HelpCommand());
            AddCommand(new ExitCommand(lifecycle));
        }

        public RootCommand(IApplication application) : this(application, application.Console)
        {
        }
    }
}