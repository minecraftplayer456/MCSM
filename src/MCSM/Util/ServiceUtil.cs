using System;

namespace MCSM.Util
{
    public class Service<T> where T : Service<T>
    {
        private static readonly Lazy<T> _default = new Lazy<T>();

        public static T Default => _default.Value;
    }
}