using System;
using System.Collections.Generic;
using MCSM.Util;
using Terminal.Gui;

namespace MCSM.Services
{
    public class UIService
    {
        public static UIService Default = new UIService();

        private readonly Dictionary<Type, View> _loadedView;

        public UIService()
        {
            _loadedView = new Dictionary<Type, View>();
        }

        public T getView<T>() where T : View
        {
            if (_loadedView.ContainsKey(typeof(T)))
            {
                return _loadedView[typeof(T)] as T;
            }

            var view = Activator.CreateInstance(typeof(T)) as T;
            _loadedView.Add(typeof(T), view);
            return view;
        }
    }
}