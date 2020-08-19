using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MCSM.Api.Ui;

namespace MCSM.Ui.Test.Util
{
    /// <summary>
    ///     Wrapper for IConsole that can use a reader and a writer for console actions
    /// </summary>
    public class TestConsole : IConsole
    {
        public TestConsole(TextWriter writer, TextReader reader)
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

    /// <summary>
    ///     Textwriter that allows you to get the output written to it
    /// </summary>
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
            //Save string and write to system console
            _content.Add(value);
            Console.WriteLine(value);
        }

        public override void Write(string value)
        {
            //Test if value ist null or a white space and the return
            if (string.IsNullOrWhiteSpace(value)) return;

            //Save string and write to system console
            _content.Add(value);
            Console.Write(value);
        }
    }

    /// <summary>
    ///     Reader that allows you to set custom read content. It reads one string in the array and on the next read it steps
    ///     on futher.
    ///     If the count is bigger than the size of the array it will return null
    /// </summary>
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