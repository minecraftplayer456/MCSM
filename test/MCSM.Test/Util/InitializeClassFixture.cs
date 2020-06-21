using System;
using MCSM.Util;

namespace MCSM.Test.Util
{
    public class InitializeClassFixture : IDisposable
    {
        public InitializeClassFixture()
        {
            LogUtil.Initialize();
        }

        public void Dispose()
        {
        }
    }
}