using System;
using System.CommandLine.IO;
using System.IO;
using System.Text;
using MCSM.Api.Ui;

namespace MCSM.Ui.Test.Util
{
    public class TestConsole : IConsole
    {
        public TestConsole(TestTextWriter writer, TestTextReader reader)
        {
            Out = writer;
            Error = writer;
            In = reader;
        }
        
        public TextWriter Out { get; }
        public TextWriter Error { get; }
        public TextReader In { get; }
        public bool IsOutRedirected => false;
        public bool IsErrorRedirected => false;
        public bool IsInRedirected => false;
        public string ReadLine()
        {
            return In.ReadLine();
        }

        public void Write(string s = "")
        {
            Out.Write(s);
            Console.Write(s);
        }

        public void WriteLine(string s = "")
        {
            Out.WriteLine(s);
            Console.WriteLine(s);
        }
    }

    public class TestTextWriter : TextWriter
    {
        private readonly StringBuilder _builder;
        
        public TestTextWriter()
        {
            _builder = new StringBuilder();
        }
        
        public override Encoding Encoding => Encoding.Default;

        public override void Write(char value)
        {
            _builder.Append(value);
        }

        public override string ToString()
        {
            return _builder.ToString();
        }
    }

    public class TestTextReader : TextReader
    {
        private readonly Func<string> _readFunc;

        public TestTextReader(Func<string> readFunc = null)
        {
            _readFunc = readFunc;
        }

        public override string ReadLine()
        {
            return _readFunc?.Invoke();
        }
    }
}