using System.CommandLine;
using System.CommandLine.Invocation;
using MCSM.Core.Test.Util;
using Xunit;

namespace MCSM.Ui.Test.Repl
{
    public class ReplTest : IClassFixture<InitializationClassFixture>
    {
        [Fact]
        public void When_NoneValidCommand_Then_ReturnNull()
        {
            var repl = new Ui.Repl.Repl(new TestCommand());
            Assert.Null(repl.ComputeInput(""));
        }

        public class TestCommand : Command
        {
            public TestCommand() : base("test", "test command")
            {
                Add(new Option<string>("-s"));

                Handler = CommandHandler.Create<string>(Execute);
            }

            public bool Executed { get; private set; }
            public string String { get; private set; }

            public void Execute(string s)
            {
                String = s;
                Executed = true;
            }
        }
    }
}