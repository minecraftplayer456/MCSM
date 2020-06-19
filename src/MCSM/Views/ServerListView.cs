using MCSM.Util;
using MCSM.ViewModels;
using Terminal.Gui;

namespace MCSM.Views
{
    public class ServerListView : ViewFor<ServerListViewModel>
    {
        public ServerListView() : base(new ServerListViewModel())
        {
            var label = new Label(0, 0, "Hello world!");
            
            base.Add(label);
        }
    }
}