using MCSM.Api;

namespace MCSM.Ui.Repl.Commands
{
    /// <summary>
    ///     Root command for repl with subcommands
    /// </summary>
    public class RootCommand : System.CommandLine.RootCommand
    {
        public RootCommand(IApplication application)
        {
            AddCommand(new VersionCommand());
            AddCommand(new HelpCommand());
            AddCommand(new ExitCommand(application));
        }
    }
}