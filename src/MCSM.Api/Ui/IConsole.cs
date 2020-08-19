using System.IO;

namespace MCSM.Api.Ui
{
    public interface IConsole
    {
        public TextWriter Out { get; }

        public TextWriter Error { get; }

        public TextReader In { get; }

        public bool IsOutRedirected { get; }

        public bool IsErrorRedirected { get; }

        public bool IsInRedirected { get; }

        public string ReadLine();

        public void Write(string s = "");

        public void WriteLine(string s = "");
    }
}