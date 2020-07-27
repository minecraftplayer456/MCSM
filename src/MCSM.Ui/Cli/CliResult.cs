namespace MCSM.Ui.Cli
{
    /// <summary>
    ///     Struct for holding data of arguments, that are parsed by cli
    /// </summary>
    public readonly struct CliResult
    {
        public CliResult(bool debug)
        {
            Debug = debug;
        }

        /// <summary>
        ///     When true root log level will be verbose
        /// </summary>
        public bool Debug { get; }
    }
}