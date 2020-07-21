using System;
using MCSM.Core.Manager.IO;
using Serilog.Events;

namespace MCSM.Core.Test.Util
{
    public class InitializationClassFixture : IDisposable
    {
        public InitializationClassFixture()
        {
            var logManager = new LogManager(LogEventLevel.Verbose);
        }

        public void Dispose()
        {
        }
    }
}