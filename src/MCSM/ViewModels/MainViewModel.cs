using MCSM.Util.UI;

namespace MCSM.ViewModels
{
    public class MainViewModel
    {
        public readonly PropertySubject<string> Name;

        public MainViewModel()
        {
            Name = new PropertySubject<string> {Value = "Test"};
        }
    }
}