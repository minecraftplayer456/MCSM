using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace MCSM.Api.Ui
{
    /// <summary>
    ///     Repl (Read evaluate print loop) for user interface.
    ///     Read: The programme waits until console input
    ///     Evaluate: If an input is registered it will be parsed and the result executed
    ///     Print: The result will be printed
    ///     Loop: The programme waits until console input again
    /// </summary>
    public interface IRepl
    {
        /// <summary>
        ///     When true repl is running
        /// </summary>
        bool Running { get; }

        /// <summary>
        ///     Starts the repl with thread. Repl can only stopped by enter the exit command
        /// </summary>
        Task Run();

        /// <summary>
        ///     Stops the repl. This method is called by exit command!
        /// </summary>
        void Exit();

        /// <summary>
        ///     Computes the input from console and executes its command
        /// </summary>
        /// <param name="input">input from console</param>
        /// <returns>parse result</returns>
        Task<ParseResult> ComputeInput(string input);
    }
}