using System.Reactive.Linq;
using MCSM.Events;
using MCSM.Util;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Reactive.Bindings.Notifiers;
using Terminal.Gui;

namespace MCSM.ViewModels
{
    public class RootViewModel : BaseViewModel
    {
        public readonly ReactiveCommand<View> ChangeMainView;
        public readonly ReadOnlyReactiveProperty<View> CurrentMainView;

        public RootViewModel()
        {
            ChangeMainView = new ReactiveCommand<View>()
                .WithSubscribe(x => MessageBroker.Default.Publish(new ChangeMainViewEvent<View>(x)))
                .AddTo(Disposables);

            CurrentMainView = MessageBroker.Default
                .ToObservable<ChangeMainViewEvent<View>>()
                .Select(x => x.View)
                .ObserveOnUIDispatcher()
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposables);
        }
    }
}