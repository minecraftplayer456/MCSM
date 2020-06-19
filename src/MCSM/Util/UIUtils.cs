using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using Reactive.Bindings;
using Terminal.Gui;

namespace MCSM.Util
{
    public interface IViewFor<TVm> where TVm : IViewModel
    {
        public ReactiveProperty<TVm> ViewModel { get; }
    }

    public abstract class ViewFor<TVm> : View, IViewFor<TVm> where TVm : IViewModel
    {
        public ViewFor(TVm viewModel)
        {
            Width = Dim.Fill();
            Height = Dim.Fill();
            
            ViewModel = new ReactiveProperty<TVm>(viewModel);
        }

        public ReactiveProperty<TVm> ViewModel { get; }
    }

    public interface IBindableBase : INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;
    }
    
    public abstract class BindableBase : IBindableBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(field)));
            return true;
        }
    }

    public interface IDisposableBase : IBindableBase, IDisposable
    {
        
    }

    public abstract class DisposablesBase : BindableBase, IDisposableBase
    {
        protected readonly CompositeDisposable Disposable = new CompositeDisposable();
        
        public void Dispose()
        {
            Disposable.Dispose();
        }
    }

    public interface IViewModel : IDisposableBase
    {
        public ReactiveProperty<string> ViewName { get; }
    }

    public abstract class ViewModelBase : DisposablesBase, IViewModel
    {
        public ViewModelBase(string viewName)
        {
            ViewName = new ReactiveProperty<string>(viewName);
        }

        public ViewModelBase() : this("") { }
        public ReactiveProperty<string> ViewName { get; }
    }
}