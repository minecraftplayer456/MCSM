using System.CommandLine;
using MCSM.Core.Test.Util;
using MCSM.Ui.Util;
using Xunit;
using Xunit.Abstractions;

namespace MCSM.Ui.Test.Repl
{
    public class ReplTest : BaseTest
    {
        public ReplTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void When_NoneValidCommand_Then_ReturnNull()
        {
            var console = new Console();
            var repl = new Ui.Repl.Repl(console, new DummyCommand());

            Assert.Null(repl.ComputeInput("").Result);
        }

        public class DummyCommand : Command
        {
            public DummyCommand() : base("dummy", "dummy command")
            {
            }
        }
    }
}