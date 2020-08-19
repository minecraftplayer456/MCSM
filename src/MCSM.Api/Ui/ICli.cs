namespace MCSM.Api.Ui
{
    public interface ICli
    {
        /// <summary>
        ///     Parse arguments and get cli result
        /// </summary>
        /// <param name="args">args as string array from main method</param>
        /// <returns>parsed arguments as cli result</returns>
        CliResult Parse(string[] args);
    }

    /// <summary>
    ///     Struct for holding data of arguments, that are parsed by cli
    /// </summary>
    public struct CliResult
    {
        /// <summary>
        ///     When true root log level will be verbose
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        ///     When true no repl will be started
        /// </summary>
        public bool NoRepl { get; set; }
    }
}