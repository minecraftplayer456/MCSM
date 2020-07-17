using System;
using System.Linq;
using MCSM.Ui.Util;
using MCSM.Ui.ViewModel;
using Terminal.Gui;

namespace MCSM.Ui.Views
{
    public class MainView : Toplevel, IViewFor<MainViewModel>
    {
        public MainView()
        {
            ViewModel = new MainViewModel();

            var menuBar = new MenuBar(new[]
            {
                new MenuBarItem("Test1", "",
                    () => { ViewModel.ChangeViewTo.Execute(new TestView(new TestViewModel("Hi1"))); }),
                new MenuBarItem("Test2", "",
                    () => { ViewModel.ChangeViewTo.Execute(new TestView(new TestViewModel("Hi2"))); })
            });
            var window = new Window();

            ViewModel.CurrentView.Subscribe(view =>
            {
                window.Subviews.ToList().ForEach(subView => subView.Dispose());
                window.RemoveAll();
                if (view != null) window.Add(view);
            });

            var statusBar = new StatusBar();

            base.Add(window);
            base.Add(menuBar);
            base.Add(statusBar);
        }

        public MainViewModel ViewModel { get; }
    }
}