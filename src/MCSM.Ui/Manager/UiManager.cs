using Serilog;

namespace MCSM.Ui.Manager
{
    public interface IUiManager
    {
        void OpenShell();

        void CloseShell();
    }

    public class UiManager : IUiManager
    {
        private readonly ILogger _log;

        public UiManager()
        {
            _log = Log.ForContext<UiManager>();
        }

        public void OpenShell()
        {
            _log.Debug("Open shell");
        }

        public void CloseShell()
        {
            _log.Debug("Close shell");
        }
    }
}