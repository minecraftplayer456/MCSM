using MCSM.Util;
using Terminal.Gui;

namespace MCSM.Events
{
    public class MainViewChangedEvent
    {
        public readonly IViewFor<BaseViewModel> NewMainView;
        
        public MainViewChangedEvent(IViewFor<BaseViewModel> newMainView)
        {
            NewMainView = newMainView;
        }
    }
}