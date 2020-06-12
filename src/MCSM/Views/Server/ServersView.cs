using System.Collections.Generic;
using MCSM.Util;
using MCSM.ViewModels;
using MCSM.ViewModels.Server;
using Reactive.Bindings.Notifiers;
using Terminal.Gui;

namespace MCSM.Views.Server
{
    public class ServersView : View, IViewFor<ServersViewModel>
    {
        public ServersView()
        {
            X = 0;
            Y = 0;
            Width = Dim.Fill();
            Height = Dim.Fill();

            ViewModel = new ServersViewModel();

            var serverList = new ListView(new List<string>
                {"Survival Server", "Creative Server", "Building Server", "Mod Server"})
            {
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            base.Add(serverList);
        }

        public ServersViewModel ViewModel { get; }
    }
}