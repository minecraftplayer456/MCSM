using Terminal.Gui;

namespace MCSM.Views
{
    /// <summary>
    ///     Root view which is static with a menubar. Only the child view changes.
    /// </summary>
    public class RootView : Toplevel
    {
        public RootView() : base(new Rect(0, 0, Driver.Cols, Driver.Rows))
        {
            var menu = new MenuBar(new MenuBarItem[] { });

            var mainView = new MainView();

            base.Add(mainView);
            base.Add(menu);
        }
    }
}