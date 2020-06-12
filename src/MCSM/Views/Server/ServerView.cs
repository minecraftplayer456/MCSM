using MCSM.Util;
using MCSM.ViewModels;
using MCSM.ViewModels.Server;
using Terminal.Gui;

namespace MCSM.Views.Server
{
    public class ServerView : View, IViewFor<ServerViewModel>
    {
        public ServerView()
        {
            X = 0;
            Y = 0;
            Height = Dim.Fill();
            Width = Dim.Fill();
            
            ViewModel = new ServerViewModel();
            
            var label = new Label("Hi");
            base.Add(label);
        }

        public ServerViewModel ViewModel { get; }
    }
}