using System.Reactive.Linq;
using MCSM.Events;
using MCSM.Util;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Reactive.Bindings.Notifiers;
using Terminal.Gui;

namespace MCSM.ViewModels
{
    /// <summary>
    ///     View model for root toplevel view
    /// </summary>
    public class RootViewModel : BaseViewModel
    {
        public readonly ReadOnlyReactiveProperty<IViewFor<BaseViewModel>> CurrentMainView;

        public readonly ReactiveCommand<IViewFor<BaseViewModel>> ChangeMainView;

        public RootViewModel() : base(null)
        {
            ChangeMainView = new ReactiveCommand<IViewFor<BaseViewModel>>()
                .WithSubscribe(x => MessageBroker.Default.Publish(new MainViewChangedEvent(x)))
                .AddTo(Disposables);

            CurrentMainView = MessageBroker.Default.ToObservable<MainViewChangedEvent>()
                .Select(x => x.NewMainView)
                .ObserveOnUIDispatcher()
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposables);
        }
    }
}