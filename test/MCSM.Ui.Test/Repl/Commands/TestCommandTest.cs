using MCSM.Ui.Repl.Commands;
using Xunit;

namespace MCSM.Ui.Test.Repl.Commands
{
    public class TestCommandTest
    {
        [Fact]
        public void When_CommandValid_Then_Execute()
        {
            var repl = new Ui.Repl.Repl();
            var result = repl.ComputeInput("test -s string");
            var command = result.CommandResult.Command as TestCommand;

            Assert.NotNull(command);
            Assert.Equal("string", command.String);
        }
    }
}