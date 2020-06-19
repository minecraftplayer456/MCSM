using System;
using System.Collections.Generic;
using MCSM.Util;

namespace MCSM.Services
{
    public class UIService : Service<UIService>
    {
        private readonly Dictionary<Type, IViewFor<IViewModel>> _views;

        public UIService()
        {
            _views = new Dictionary<Type, IViewFor<IViewModel>>();
        }
        
        public T getView<T>() where T : IViewFor<IViewModel>, new()
        {
            if (_views.ContainsKey(typeof(T)))
            {
                return (T) _views[typeof(T)];
            }

            var view = new T();
            _views.Add(typeof(T), view);
            return view;
        }
    }
}