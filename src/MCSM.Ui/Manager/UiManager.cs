using System;
using System.Collections.Generic;
using MCSM.Ui.Util.Ui;
using MCSM.Ui.Views;

namespace MCSM.Ui.Manager
{
    public interface IUiManager
    {
        void OpenUi();

        void CloseUi();

        Type ResolveViewType(IReactiveObject viewModel);

        IViewFor<IReactiveObject> ResolveView(IReactiveObject viewModel);
    }

    public class UiManager : IUiManager
    {
        //IViewFor
        private readonly Dictionary<Type, IViewFor<IReactiveObject>> _views;

        //ReactiveObject, IViewFor
        private readonly Dictionary<Type, Type> _viewTypes;

        public UiManager()
        {
            _viewTypes = new Dictionary<Type, Type>();
            _views = new Dictionary<Type, IViewFor<IReactiveObject>>();
        }

        public Type ResolveViewType(IReactiveObject viewModel)
        {
            var viewModelType = viewModel.GetType();

            if (_viewTypes.ContainsKey(viewModelType)) return _viewTypes[viewModelType];

            var viewName = viewModel.GetType().Name.Replace("ViewModel", "View");

            var viewType = Type.GetType(viewName);
            if (viewType == null) return null;

            _viewTypes.Add(viewModelType, viewType);
            return viewType;
        }

        public IViewFor<IReactiveObject> ResolveView(IReactiveObject viewModel)
        {
            var viewType = ResolveViewType(viewModel);
            if (viewType == null) return null;

            if (_views.ContainsKey(viewType)) return _views[viewType];

            var view = Activator.CreateInstance(viewType) as IViewFor<IReactiveObject>;

            view.ViewModel = viewModel;

            return view;
        }

        public void OpenUi()
        {
            Terminal.Gui.Application.Run<AppView>();
        }

        public void CloseUi()
        {
            Terminal.Gui.Application.Shutdown();
        }
    }
}