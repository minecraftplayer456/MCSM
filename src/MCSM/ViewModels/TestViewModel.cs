using MCSM.Util;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MCSM.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        public readonly ReadOnlyReactiveProperty<string> Text;

        public TestViewModel(string text)
        {
            Text = new ReactiveProperty<string>(text)
                .AddTo(Disposables)
                .ToReadOnlyReactiveProperty();
        }
    }
}