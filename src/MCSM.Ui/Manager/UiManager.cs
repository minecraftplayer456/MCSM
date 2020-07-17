using MCSM.Ui.Views;

namespace MCSM.Ui.Manager
{
    public interface IUiManager
    {
        void OpenUi();

        void CloseUi();
    }

    public class UiManager : IUiManager
    {
        public void OpenUi()
        {
            Terminal.Gui.Application.Run<MainView>();
        }

        public void CloseUi()
        {
            Terminal.Gui.Application.Shutdown();
        }
    }
}