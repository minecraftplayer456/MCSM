using MCSM.Views;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Terminal.Gui;
using Constants = MCSM.Util.Constants;

namespace MCSM
{
    public class App
    {
        private void Start()
        {
            var consoleLogLevelSwitch = new LoggingLevelSwitch(Constants.DefaultLogLevel);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(Constants.DefaultLogLevel)
                .WriteTo.Console(levelSwitch: consoleLogLevelSwitch)
                .WriteTo.File("logs/latest.txt")
                .CreateLogger();

            Log.Information(Constants.MCSMShortNameVersion);

            consoleLogLevelSwitch.MinimumLevel = LogEventLevel.Fatal;
            Application.Run<RootView>();
        }

        public static void Main(string[] args)
        {
            var app = new App();
            app.Start();
        }
    }
}