using System.IO;

namespace MCSM.Api.Ui
{
    /// <summary>
    ///     Abstractions of system console to hock up on this at testing.
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        ///     Gets the text writer for writing to normal system output
        /// </summary>
        public TextWriter Out { get; }

        /// <summary>
        ///     Gets the text writer for writing to error system ouput
        /// </summary>
        public TextWriter Error { get; }

        /// <summary>
        ///     Gets the text reader for command line input
        /// </summary>
        public TextReader In { get; }

        /// <summary>
        ///     True if the output stream has been redirected
        /// </summary>
        public bool IsOutRedirected { get; }

        /// <summary>
        ///     True if the error stream has been redirected
        /// </summary>
        public bool IsErrorRedirected { get; }

        /// <summary>
        ///     True if the input stream has been redirected
        /// </summary>
        public bool IsInRedirected { get; }

        /// <summary>
        ///     Reads one line from the command line with In TextReader
        /// </summary>
        /// <returns></returns>
        public string ReadLine();

        /// <summary>
        ///     Write a string to command line. Writing after that will occur on the same line!
        /// </summary>
        /// <param name="s"></param>
        public void Write(string s = "");

        /// <summary>
        ///     Writes a string at one line. Writing after that will occur on the next line
        /// </summary>
        /// <param name="s"></param>
        public void WriteLine(string s = "");
    }
}