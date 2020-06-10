using Reactive.Bindings;

namespace MCSM.ViewModels
{
    public class MainViewModel
    {
        public readonly ReactiveProperty<string> Input;
        public readonly ReadOnlyReactiveProperty<string> Output;

        public MainViewModel()
        {
            Input = new ReactiveProperty<string>("");
            Output = Input.ToReadOnlyReactiveProperty();
        }
    }
}