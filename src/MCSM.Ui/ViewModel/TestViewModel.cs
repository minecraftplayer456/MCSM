using MCSM.Ui.Util.Ui;
using Reactive.Bindings;

namespace MCSM.Ui.ViewModel
{
    public class TestViewModel : ReactiveObject, IRoutedReactiveObject
    {
        public readonly ReactiveProperty<string> Text;

        public TestViewModel(string text)
        {
            Text = new ReactiveProperty<string>(text);
        }
    }
}