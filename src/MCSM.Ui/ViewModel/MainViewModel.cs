using System.Reactive.Linq;
using MCSM.Ui.Event;
using MCSM.Ui.Util;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Reactive.Bindings.Notifiers;
using Terminal.Gui;

namespace MCSM.Ui.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        public readonly ReactiveCommand<View> ChangeViewTo;
        public readonly ReadOnlyReactiveProperty<View> CurrentView;

        public MainViewModel()
        {
            ChangeViewTo = new ReactiveCommand<View>()
                .WithSubscribe(x => MessageBroker.Default.Publish(new ChangeViewToEvent<View>(x)))
                .AddTo(Disposables);

            CurrentView = MessageBroker.Default
                .ToObservable<ChangeViewToEvent<View>>()
                .Select(x => x.View)
                .ObserveOnUIDispatcher()
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposables);
        }
    }
}