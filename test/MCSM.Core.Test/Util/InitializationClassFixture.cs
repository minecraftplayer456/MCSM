using MCSM.Core.Manager.IO;
using Serilog.Events;

namespace MCSM.Core.Test.Util
{
    public class InitializationClassFixture
    {
        public InitializationClassFixture()
        {
            new LogManager(LogEventLevel.Verbose);
        }
    }
}