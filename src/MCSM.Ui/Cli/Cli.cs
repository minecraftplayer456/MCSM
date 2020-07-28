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

            var debug = result.ValueForOption<bool>("--debug");

            return new CliResult(debug);
        }
    }
}