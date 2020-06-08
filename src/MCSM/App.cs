using MCSM.Views;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Terminal.Gui;
using Constants = MCSM.Util.Constants;

namespace MCSM
{
    /// <summary>
    /// Entry point and central point for the application.
    /// </summary>
    public class App
    {
        /// <summary>
        /// Initialization and loading of app
        /// </summary>
        public void Start()
        {
            //Initialize Logger
            var logLevelSwitch = new LoggingLevelSwitch(Constants.DefaultLogLevel);
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(logLevelSwitch)
                .WriteTo.Console()
                .WriteTo.File("logs/latest.txt")
                .CreateLogger();
            
            Log.Information("Starting MCSM - {version}", Constants.MCSMVersion);

            //Open root view
            logLevelSwitch.MinimumLevel = LogEventLevel.Fatal;
            Application.UseSystemConsole = true;
            Application.Run<RootView>();
        }
        
        /// <summary>
        /// Entry Method for the application
        /// </summary>
        /// <param name="args">Programme arguments from the user</param>
        public static void Main(string[] args)
        {
            //Initialize App
            var app = new App();
            app.Start();
        }
    }
}