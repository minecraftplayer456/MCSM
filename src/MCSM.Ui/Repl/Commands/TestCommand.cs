using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MCSM.Ui.Repl.Commands
{
    public class TestCommand : Command
    {
        public TestCommand() : base("test", "test command")
        {
            Add(new Option<string>("-s", "Test"));

            Handler = CommandHandler.Create<string>(Execute);
        }

        public string String { get; private set; }

        public void Execute(string s)
        {
            Console.WriteLine(s);
            String = s;
        }
    }
}