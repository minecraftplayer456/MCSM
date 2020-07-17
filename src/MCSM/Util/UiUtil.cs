using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MCSM.Util
{
    public interface IViewFor<TVm> where TVm : BaseViewModel
    {
        public TVm ViewModel { get; }
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
        protected readonly CompositeDisposable Disposables;
        public readonly ReactiveProperty<string> ViewName;

        protected BaseViewModel(string viewName) : this()
        {
            ViewName = new ReactiveProperty<string>(viewName)
                .AddTo(Disposables);
        }

        protected BaseViewModel()
        {
            Disposables = new CompositeDisposable();
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}