using System.CommandLine.Builder;

namespace MCSM.Ui.Cli
{
    /// <summary>
    ///     Command line interface to parse programme arguments
    /// </summary>
    public interface ICli
    {
        /// <summary>
        ///     Parse arguments and get cli result
        /// </summary>
        /// <param name="args">args as string array from main method</param>
        /// <returns>parsed arguments as cli result</returns>
        CliResult Parse(string[] args);
    }

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