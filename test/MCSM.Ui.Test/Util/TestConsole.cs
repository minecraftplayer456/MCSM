using System;
using System.Collections.Generic;
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
        }

        public void WriteLine(string s = "")
        {
            Out.WriteLine(s);
        }
    }

    public class TestTextWriter : TextWriter
    {
        private readonly List<string> _content;

        public TestTextWriter()
        {
            _content = new List<string>();
        }

        public override Encoding Encoding => Encoding.Default;

        public string[] Content => _content.ToArray();

        public override void WriteLine(string value)
        {
            _content.Add(value);
            Console.WriteLine(value);
        }

        public override void Write(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            _content.Add(value);
            Console.Write(value);
        }
    }

    public class TestTextReader : TextReader
    {
        private readonly string[] _content;
        private int _count;

        public TestTextReader(string[] content)
        {
            _content = content;
        }

        public TestTextReader() : this(new string[] { })
        {
        }

        public override string ReadLine()
        {
            return _content.Length < _count ? null : _content[_count++];
        }
    }
}