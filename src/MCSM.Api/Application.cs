using MCSM.Api.Manager.IO;
using MCSM.Api.Ui;

namespace MCSM.Api
{
    /// <summary>
    ///     Lifecycle class to be able to hock up to methods for testing
    /// </summary>
    public interface IApplicationLifecycle
    {
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

    /// <summary>
    ///     Main application class for starting and stopping. This class holds all managers
    /// </summary>
    public interface IApplication : IApplicationLifecycle
    {
        /// <summary>
        ///     Returns current ILogManager instance
        /// </summary>
        ILogManager LogManager { get; }

        /// <summary>
        ///     Returns current IFileManager instance
        /// </summary>
        IFileManager FileManager { get; }

        /// <summary>
        ///     Returns current IRepl instance
        /// </summary>
        IRepl Repl { get; }

        /// <summary>
        ///     Returns current IConsole instance
        /// </summary>
        IConsole Console { get; }
    }
}