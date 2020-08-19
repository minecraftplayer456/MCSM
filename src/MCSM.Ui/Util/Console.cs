using System.CommandLine.IO;
using System.IO;
using MCSM.Api.Ui;

namespace MCSM.Ui.Util
{
    public class Console : IConsole
    {
        public TextWriter Out => System.Console.Out;
        public TextWriter Error => System.Console.Error;
        public TextReader In => System.Console.In;

        public bool IsOutRedirected => System.Console.IsOutputRedirected;
        public bool IsErrorRedirected => System.Console.IsErrorRedirected;
        public bool IsInRedirected => System.Console.IsInputRedirected;

        public string ReadLine()
        {
            //Read line from system console
            return System.Console.ReadLine();
        }

        public void Write(string s)
        {
            //Write to system console
            System.Console.Write(s);
        }

        public void WriteLine(string s)
        {
            //Write line to system console
            System.Console.WriteLine(s);
        }
    }

    /// <summary>
    ///     Wrapper for system.commandline.IConsole and mcsm's IConsole
    /// </summary>
    public class CommandLineConsole : System.CommandLine.IConsole
    {
        private readonly IConsole _console;

        public CommandLineConsole(IConsole console)
        {
            _console = console;

            //Creates output and error stream from IConsole Out and Error
            Out = StandardStreamWriter.Create(_console.Out);
            Error = StandardStreamWriter.Create(_console.Error);
        }

        public IStandardStreamWriter Out { get; }
        public IStandardStreamWriter Error { get; }

        public bool IsOutputRedirected => _console.IsOutRedirected;
        public bool IsErrorRedirected => _console.IsErrorRedirected;
        public bool IsInputRedirected => _console.IsInRedirected;
    }
}