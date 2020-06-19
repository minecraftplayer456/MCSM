using MCSM.Util;
using Reactive.Bindings;
using Terminal.Gui;

namespace MCSM.ViewModels
{
    public class RootViewModel : ViewModelBase
    {
        public readonly ReactiveProperty<View> CurrentView;
        
        public RootViewModel() : base("")
        {
            CurrentView = new ReactiveProperty<View>();
        }
    }
}