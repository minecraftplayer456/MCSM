using System;
using System.Threading;

namespace MCSM.Core.Test.Util
{
    public static class TestUtil
    {
        public static void WaitUntil(Func<bool> func, int timeout = 10, int wait = 1000)
        {
            var count = 0;
            while (true)
            {
                Thread.Sleep(wait);
                if (func.Invoke()) break;
                if (count == timeout) throw new Exception("Wait until took too long");
                count++;
            }
        }
    }
}