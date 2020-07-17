using System;
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
            
            var menuBar = new MenuBar(new []
            {
                new MenuBarItem("Test1", "", () =>
                {
                    ViewModel.Text.Value = "Test1";
                }),
                new MenuBarItem("Test2", "", () =>
                {
                    ViewModel.Text.Value = "Test2";
                })
            });
            var window = new Window();

            var label = new Label("Hieorwjbioperwgniorb");
            ViewModel.Text.Subscribe(text => label.Text = text);
            window.Add(label);
            
            var statusBar = new StatusBar();
            
            base.Add(window);
            base.Add(menuBar);
            base.Add(statusBar);
        }

        public MainViewModel ViewModel { get; }
    }
}