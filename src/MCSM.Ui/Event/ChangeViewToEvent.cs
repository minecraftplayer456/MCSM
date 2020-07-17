using Terminal.Gui;

namespace MCSM.Ui.Event
{
    public class ChangeViewToEvent<TV> where TV : View
    {
        public readonly TV View;

        public ChangeViewToEvent(TV view)
        {
            View = view;
        }
    }
}