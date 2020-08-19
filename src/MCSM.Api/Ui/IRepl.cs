using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace MCSM.Api.Ui
{
    /// <summary>
    ///     Repl for user interface
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