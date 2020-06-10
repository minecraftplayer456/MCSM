using System;
using MCSM.Util;
using MCSM.ViewModels;
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
            var viewModel = new RootViewModel();
            
            var menuBar = new MenuBar(new MenuBarItem[] { });
            var statusBar = new StatusBar();

            var window = new Window($"MCMS - {Constants.MCSMVersion}", 1);

            viewModel.CurrentView.Subscribe(view =>
            {
                if (view == null) return;
                window.RemoveAll();
                window.Add(view);
            });
            
            base.Add(window);
            base.Add(menuBar);
            base.Add(statusBar);
        }
    }
}