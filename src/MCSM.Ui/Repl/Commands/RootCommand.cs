namespace MCSM.Ui.Repl.Commands
{
    /// <summary>
    ///     Root command for repl with subcommands
    /// </summary>
    public class RootCommand : System.CommandLine.RootCommand
    {
        public RootCommand()
        {
            AddCommand(new TestCommand());
            AddCommand(new VersionCommand());
            AddCommand(new HelpCommand());
            AddCommand(new ExitCommand());
        }
    }
}