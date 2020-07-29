using System.CommandLine;

namespace MCSM.Ui.Cli
{
    /// <summary>
    ///     Root command with cli options
    /// </summary>
    public class RootCommand : System.CommandLine.RootCommand
    {
        public RootCommand()
        {
            Add(new Option<bool>(new[] {"--debug", "-d"}));
            Add(new Option<bool>(new []{"--no-repl", "-nr"}));
        }
    }
}