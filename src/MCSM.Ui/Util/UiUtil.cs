using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using Reactive.Bindings;

namespace MCSM.Ui.Util
{
    public interface IViewFor<TVm> where TVm : ReactiveObject
    {
        public TVm ViewModel { get; }
    }

    public abstract class ReactiveObject : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected readonly CompositeDisposable Disposables = new CompositeDisposable();

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }

    public class NamedViewModel : ReactiveObject
    {
        public readonly ReactiveProperty<string> ViewName;

        public NamedViewModel(string name)
        {
            ViewName = new ReactiveProperty<string>(name);
        }
    }
}