using System;
using MCSM.Services;
using MCSM.Util;
using MCSM.ViewModels;
using MCSM.Views.Server;
using Terminal.Gui;

namespace MCSM.Views
{
    public class RootView : Toplevel, IViewFor<RootViewModel>
    {
        public RootView() : base(new Rect(0, 0, Driver.Cols, Driver.Rows))
        {
            ViewModel = new RootViewModel();

            var menuBar = new MenuBar(new MenuBarItem[]
            {
                new MenuBarItem("Servers", "", () =>
                {
                    ViewModel.ChangeMainView.Execute(UIService.Default.getView<ServersView>());
                }),  
                new MenuBarItem("Server", "", () =>
                {
                    ViewModel.ChangeMainView.Execute(UIService.Default.getView<ServerView>());
                })
            });
            
            var statusBar = new StatusBar();

            var window = new Window(Constants.MCSMShortNameVersion, 1);

            ViewModel.CurrentMainView.Subscribe(view =>
            {
                if (view == null) return;

                window.RemoveAll();
                window.Add(view as View);
                window.Title = Constants.MCSMShortNameVersion + " - " + view.ViewModel.ViewName.Value;
            });

            base.Add(window);
            base.Add(menuBar);
            base.Add(statusBar);
        }

        public RootViewModel ViewModel { get; }
    }
}