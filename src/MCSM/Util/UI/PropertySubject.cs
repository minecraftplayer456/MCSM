using System;
using System.Reactive.Subjects;

namespace MCSM.Util.UI
{
    public class PropertySubject<T> : ISubject<T>
    {
        private readonly Subject<T> _subject = new Subject<T>();
        private T _value;

        public T Value
        {
            get => _value;
            set => SetValue(value);
        }

        public void OnCompleted()
        {
            _subject.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _subject.OnError(error);
        }

        public void OnNext(T value)
        {
            SetValue(value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _subject.Subscribe(observer);
        }

        private void SetValue(T value)
        {
            _value = value;
            _subject.OnNext(value);
        }
    }
}