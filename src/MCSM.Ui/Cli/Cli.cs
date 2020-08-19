using System.CommandLine.Builder;
using MCSM.Api.Ui;

namespace MCSM.Ui.Cli
{
    public class Cli : ICli
    {
        public CliResult Parse(string[] args)
        {
            //Create new parser with default options
            var parser = new CommandLineBuilder(new RootCommand())
                .UseDefaults()
                .Build();

            //Parses the command line input and get results
            var result = parser.Parse(args).RootCommandResult;

            //Returns the results in CliResult class
            return new CliResult
            {
                Debug = result.ValueForOption<bool>("--debug"),
                NoRepl = result.ValueForOption<bool>("--no-repl")
            };
        }
    }
}