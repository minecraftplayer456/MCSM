using MCSM.Ui.Util;
using Reactive.Bindings;

namespace MCSM.Ui.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        public readonly ReactiveProperty<string> Text;

        public MainViewModel()
        {
            Text = new ReactiveProperty<string>("");
        }
    }
}