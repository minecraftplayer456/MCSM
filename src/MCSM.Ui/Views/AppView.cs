using MCSM.Ui.Util.Ui;
using MCSM.Ui.ViewModel;
using Terminal.Gui;

namespace MCSM.Ui.Views
{
    public class AppView : Toplevel, IViewFor<AppViewModel>
    {
        public AppView()
        {
            var menuBar = new MenuBar(new[]
            {
                new MenuBarItem("Test1", "",
                    () => { }),
                new MenuBarItem("Test2", "",
                    () => { })
            });
            var window = new Window();


            var statusBar = new StatusBar();

            base.Add(window);
            base.Add(menuBar);
            base.Add(statusBar);
        }

        public AppViewModel ViewModel { get; }
    }
}