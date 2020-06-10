using Reactive.Bindings;
using Terminal.Gui;

namespace MCSM.ViewModels
{
    public class RootViewModel
    {
        public readonly ReactiveProperty<View> CurrentView;

        public RootViewModel()
        {
            CurrentView = new ReactiveProperty<View>();
        }
    }
}