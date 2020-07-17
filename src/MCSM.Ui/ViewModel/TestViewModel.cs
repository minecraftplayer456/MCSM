using MCSM.Ui.Util;
using Reactive.Bindings;

namespace MCSM.Ui.ViewModel
{
    public class TestViewModel : NamedViewModel
    {
        public readonly ReactiveProperty<string> Text;

        public TestViewModel(string text) : base("TestView")
        {
            Text = new ReactiveProperty<string>(text);
        }
    }
}