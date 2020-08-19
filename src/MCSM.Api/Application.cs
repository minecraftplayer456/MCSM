using System.Threading.Tasks;
using MCSM.Api.Manager.IO;
using MCSM.Api.Ui;

namespace MCSM.Api
{
    public interface IApplicationLifecycle
    {
        /// <summary>
        ///     Starts the application and loads workspace
        ///     <param name="args">Programme arguments to parse with cli</param>
        /// </summary>
        Task Start(string[] args);

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

        IConsole Console { get; }
    }
}