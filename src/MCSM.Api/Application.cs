using MCSM.Api.Manager.IO;
using MCSM.Api.Ui;

namespace MCSM.Api
{
    /// <summary>
    ///     Main application class for starting and stopping. This class holds all managers
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        ///     Returns current ILogManager
        /// </summary>
        ILogManager LogManager { get; }

        /// <summary>
        ///     Returns current IFileManager
        /// </summary>
        IFileManager FileManager { get; }

        /// <summary>
        ///     Returns current IRepl
        /// </summary>
        IRepl Repl { get; }

        /// <summary>
        ///     Starts the application and loads workspace
        ///     <param name="args">Programme arguments to parse with cli</param>
        /// </summary>
        void Start(string[] args);

        /// <summary>
        ///     Stops the application and saves workspace
        /// </summary>
        void Stop();
    }
}