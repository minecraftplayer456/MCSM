using MCSM.Util;
using Terminal.Gui;

namespace MCSM.Views
{
    /// <summary>
    /// Main view kind of a dashboard
    /// </summary>
    public class MainView : Window
    {
        public MainView() : base($"MCMS - {Constants.MCSMVersion}")
        {
            X = 0;
            Y = 1;
            Height = Dim.Fill() - 1;
            Width = Dim.Fill();
        }
    }
}