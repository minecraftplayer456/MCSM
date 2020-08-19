using System.CommandLine.Builder;
using MCSM.Api.Ui;

namespace MCSM.Ui.Cli
{
    public class Cli : ICli
    {
        public CliResult Parse(string[] args)
        {
            var parser = new CommandLineBuilder(new RootCommand())
                .UseDefaults()
                .Build();

            var result = parser.Parse(args).RootCommandResult;

            return new CliResult
            {
                Debug = result.ValueForOption<bool>("--debug"),
                NoRepl = result.ValueForOption<bool>("--no-repl")
            };
        }
    }
}