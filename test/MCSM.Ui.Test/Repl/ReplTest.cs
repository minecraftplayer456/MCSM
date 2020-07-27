using System.CommandLine;
using System.CommandLine.Invocation;
using MCSM.Core.Test.Util;
using Xunit;

namespace MCSM.Ui.Test.Repl
{
    public class ReplTest : IClassFixture<InitializationClassFixture>
    {
        [Fact]
        public void When_StartingCorrectly_Then_Running()
        {
            var repl = new Ui.Repl.Repl(new TestCommand());

            repl.Run();

            TestUtil.WaitUntil(() => repl.Running);
        }

        [Fact]
        public void When_StoppingCorrectly_Then_NotRunning()
        {
            var repl = new Ui.Repl.Repl(new TestCommand());

            repl.Run();

            TestUtil.WaitUntil(() => repl.Running);

            repl.Stop();

            TestUtil.WaitUntil(() => !repl.Running);
        }

        [Fact]
        public void When_ValidCommand_Then_Execute()
        {
            var repl = new Ui.Repl.Repl(new TestCommand());
            var result = repl.ComputeInput("test -s string");
            var command = result.CommandResult.Command as TestCommand;

            Assert.NotNull(command);
            Assert.True(command.Executed);
            Assert.Equal("string", command.String);
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