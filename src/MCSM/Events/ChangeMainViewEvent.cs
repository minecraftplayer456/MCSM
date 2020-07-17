using Terminal.Gui;

namespace MCSM.Events
{
    public class ChangeMainViewEvent<TV> where TV : View
    {
        public readonly TV View;

        public ChangeMainViewEvent(TV view)
        {
            View = view;
        }
    }
}