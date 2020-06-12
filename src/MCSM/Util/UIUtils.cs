using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using Reactive.Bindings;
using Terminal.Gui;

namespace MCSM.Util
{
    public interface IViewFor<out T> where T : BaseViewModel
    {
        public T ViewModel { get; }
    }
    
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }

    public abstract class BaseViewModel : BindableBase, IDisposable
    {
        public readonly ReactiveProperty<string> ViewName;

        protected BaseViewModel(string viewName)
        {
            ViewName = new ReactiveProperty<string>(viewName);
        }

        protected CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}