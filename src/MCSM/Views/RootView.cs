using System;
using System.Linq;
using MCSM.Util;
using MCSM.ViewModels;
using Terminal.Gui;

namespace MCSM.Views
{
    public class RootView : Toplevel, IViewFor<RootViewModel>
    {
        public RootView()
        {
            ViewModel = new RootViewModel();

            var menuBar = new MenuBar(new[]
            {
                new MenuBarItem("Test1", "",
                    () => { ViewModel.ChangeMainView.Execute(new TestView(new TestViewModel("Test1"))); }),
                new MenuBarItem("Test2", "",
                    () => { ViewModel.ChangeMainView.Execute(new TestView(new TestViewModel("Test2"))); })
            });

            var statusBar = new StatusBar();

            var window = new Window("MCSM");

            ViewModel.CurrentMainView.Subscribe(view =>
            {
                window.Subviews.First().Dispose();
                window.RemoveAll();
                if (view != null) window.Add(view);
            });

            base.Add(window);
            base.Add(menuBar);
            base.Add(statusBar);
        }

        public RootViewModel ViewModel { get; }
    }
}