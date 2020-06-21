using System;

namespace MCSM.Util
{
    /// <summary>
    ///     Basic class to make an object lazy
    /// </summary>
    /// <typeparam name="T">Type of object self</typeparam>
    public abstract class LazyAble<T>
    {
        private static readonly Lazy<T> Lazy = new Lazy<T>();

        public static T Default => Lazy.Value;
    }
}