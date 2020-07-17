using MCSM.Util;
using MCSM.Util.IO;
using MCSM.Views;
using Serilog;
using Terminal.Gui;

namespace MCSM
{
    /// <summary>
    ///     Main entry point for application
    /// </summary>
    public class App
    {
        /// <summary>
        ///     Starting the application with gui
        /// </summary>
        public void Start()
        {
            LogUtil.Initialize();

            Log.Information("Starting MCSM - Version {version}", Constants.ProgrammeVersion);

            LogUtil.SwitchToUi();

            Application.Run<RootView>();
        }

        public static void Main(string[] args)
        {
            var app = new App();
            app.Start();
        }
    }
}