using MCSM.Services;
using Terminal.Gui;
using MCSM.Util;
using MCSM.ViewModels;
using Reactive.Bindings;

namespace MCSM.Views
{
    public class RootView : Toplevel, IViewFor<RootViewModel>
    {
        public RootView()
        {
            ViewModel = new ReactiveProperty<RootViewModel>(new RootViewModel());
            
            var window = new Window();

            var view = UIService.Default.getView<ServerListView>();
            
            window.Add(view);
            
            base.Add(window);
        }

        public ReactiveProperty<RootViewModel> ViewModel { get; }
    }
}