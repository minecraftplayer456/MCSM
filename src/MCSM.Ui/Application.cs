using MCSM.Ui.Manager;

namespace MCSM.Ui
{
    public class Application
    {
        private IUiManager _uiManager;

        public Application(IUiManager uiManager = null)
        {
            if(uiManager == null) _uiManager = new UiManager();
        }
        
        public void Start()
        {
            _uiManager.OpenUi();
        }
    }
}