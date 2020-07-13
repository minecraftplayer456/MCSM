using System;
using MCSM.Util.IO;

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