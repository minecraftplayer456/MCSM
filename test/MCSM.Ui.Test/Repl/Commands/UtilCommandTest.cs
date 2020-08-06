using MCSM.Api.Util;
using MCSM.Core.Test.Util;
using MCSM.Ui.Repl.Commands;
using Xunit;
using MCSM.Ui.Test.Util;
using Xunit.Abstractions;

namespace MCSM.Ui.Test.Repl.Commands
{
    public class UtilCommandTest : BaseTest
    {
        [Fact]
        public void When_ValidHelpCommand_Then_ShowHelp()
        {
            var writer = new TestTextWriter();
            var console = new TestConsole(writer, new TestTextReader());
            var repl = new Ui.Repl.Repl(console, new RootCommand(new TestApplicationLifecycle(), console));
            
            repl.ComputeInput("help");

            var content = writer.Content;
            Assert.Contains("Usage:", content);
            Assert.Contains("help", content);
            Assert.Contains("Shows information about all commands", content);
        }

        [Fact]
        public void When_ValidVersionCommand_Then_ShowVersion()
        {
            var writer = new TestTextWriter();
            var console = new TestConsole(writer, new TestTextReader());
            var repl = new Ui.Repl.Repl(console, new RootCommand(new TestApplicationLifecycle(), console));
            
            repl.ComputeInput("version");

            var content = writer.Content;
            Assert.Contains(Constants.McsmVersion, content);
        }

        [Fact]
        public void When_ValidExitCommand_Then_Exit()
        {
            var writer = new TestTextWriter();
            var console = new TestConsole(writer, new TestTextReader());
            var repl = new Ui.Repl.Repl(console, new RootCommand(new TestApplicationLifecycle(), console));
            
            repl.ComputeInput("exit");
        }

        public UtilCommandTest(ITestOutputHelper output) : base(output)
        {
        }
    }
}