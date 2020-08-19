using System;
using System.IO;
using System.Text;
using MCSM.Core.Manager.IO;
using Serilog.Events;
using Xunit.Abstractions;

namespace MCSM.Core.Test.Util
{
    /// <summary>
    ///     Abstract base class for all tests that will set the output
    /// </summary>
    public abstract class BaseTest
    {
        private static bool _initialized;

        protected BaseTest(ITestOutputHelper output)
        {
            if (!_initialized)
            {
                new LogManager(LogEventLevel.Verbose);
                _initialized = true;
            }

            // Bind console out and error to test output writer
            var writer = new TestOutputWriter(output);
            Console.SetOut(writer);
            Console.SetError(writer);
        }
    }

    /// <summary>
    ///     Warpper class for textWriter to write at test output
    /// </summary>
    public class TestOutputWriter : TextWriter
    {
        private readonly ITestOutputHelper _output;
        private StringBuilder _builder;

        public TestOutputWriter(ITestOutputHelper output)
        {
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
            // Wait until \r or \n and print composed string
            _builder ??= new StringBuilder();
            if (value.Contains("\n") || value.Contains("\r"))
            {
                value = value.Replace("\n", "");
                value = value.Replace("\r", "");
                _builder.Append(value);
                _output.WriteLine(_builder.ToString());
                _builder.Clear();
            }
            else
            {
                _builder.Append(value);
            }
        }

        public override void Write(char value)
        {
            // Redirect to Write for string
            Write(value.ToString());
        }
    }
}