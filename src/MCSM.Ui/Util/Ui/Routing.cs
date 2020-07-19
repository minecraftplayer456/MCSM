using MCSM.Ui.Manager;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MCSM.Ui.Util.Ui
{
    public interface IRoutedReactiveObject : IReactiveObject
    {
    }

    public interface IScreenReactiveObject : IReactiveObject
    {
        public IReactiveProperty<IViewFor<IReactiveObject>> CurrentView { get; }
        public IReactiveProperty<IReactiveObject> CurrentViewModel { get; }

        public ReactiveCommand<IReactiveObject> ChangeViewTo { get; }
    }

    public class ScreenReactiveObject : ReactiveObject, IScreenReactiveObject
    {
        protected readonly IUiManager _uiManager;

        public ScreenReactiveObject(IUiManager uiManager)
        {
            _uiManager = uiManager;

            CurrentView = new ReactiveProperty<IViewFor<IReactiveObject>>()
                .AddTo(Disposables);
            CurrentViewModel = new ReactiveProperty<IReactiveObject>()
                .AddTo(Disposables);

            ChangeViewTo = new ReactiveCommand<IReactiveObject>()
                .WithSubscribe(viewModel =>
                {
                    var view = uiManager.ResolveView(viewModel);
                    if (view == null) return;
                    CurrentView.Value = view;
                    CurrentViewModel.Value = viewModel;
                });
        }

        public IReactiveProperty<IViewFor<IReactiveObject>> CurrentView { get; }
        public IReactiveProperty<IReactiveObject> CurrentViewModel { get; }
        public ReactiveCommand<IReactiveObject> ChangeViewTo { get; }
    }
}