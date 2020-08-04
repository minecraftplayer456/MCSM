using System.IO;
using System.Text;
using MCSM.Core.Manager.IO;
using Serilog.Events;
using Xunit.Abstractions;

namespace MCSM.Core.Test.Util
{
    public abstract class BaseTest
    {
        public static bool Initialized;
        
        public BaseTest(ITestOutputHelper _output)
        {
            if (!Initialized)
            {
                new LogManager(LogEventLevel.Verbose);
            }
        }
    }

    public class TestOutputWriter : TextWriter
    {
        private readonly ITestOutputHelper _output;

        public TestOutputWriter(ITestOutputHelper output)
        {
            _output = output;
        }
        
        public override Encoding Encoding => Encoding.Default;

        public override void WriteLine(string value)
        {
            _output.WriteLine(value);
        }

        public override void Write(string value)
        {
            
        }
    }
}