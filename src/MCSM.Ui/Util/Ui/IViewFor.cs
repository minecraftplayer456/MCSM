namespace MCSM.Ui.Util.Ui
{
    public interface IViewFor<TVm> where TVm : IReactiveObject
    {
        public TVm ViewModel { get; }
    }
}