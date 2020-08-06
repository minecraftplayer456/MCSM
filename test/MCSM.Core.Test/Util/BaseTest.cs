using System;
using System.IO;
using System.Text;
using MCSM.Core.Manager.IO;
using Serilog;
using Serilog.Events;
using Xunit.Abstractions;

namespace MCSM.Core.Test.Util
{
    public abstract class BaseTest
    {
        public static bool Initialized;
        
        public BaseTest(ITestOutputHelper output)
        {
            if (!Initialized)
            {
                new LogManager(LogEventLevel.Verbose);
                Initialized = true;
            }
            
            var writer = new TestOutputWriter(output);
            Console.SetOut(writer);
            Console.SetError(writer);
        }
    }

    public class TestOutputWriter : TextWriter
    {
        private readonly ILogger _log;
        private readonly ITestOutputHelper _output;
        private StringBuilder _builder;

        public TestOutputWriter(ITestOutputHelper output)
        {
            _log = Log.ForContext<TestOutputWriter>();
            _output = output;
        }
        
        public override Encoding Encoding => Encoding.Default;

        public override void WriteLine(string value)
        {
            _output.WriteLine(value);
        }

        public override void WriteLine(string format, params object[] args)
        {
            _output.WriteLine(format, args);
        }

        public override void Write(string value)
        {
            _builder ??= new StringBuilder();
            if (value.Contains("\n") || value.Contains("\r"))
            {
                _builder.Append(value);
                _output.WriteLine(_builder.ToString());
            }
            else
            {
                _builder.Append(value);
            }
        }
        
        public override void Write(char value)
        {
            _log.Warning("Writing char to test output: {value}", value);
        }
    }
}