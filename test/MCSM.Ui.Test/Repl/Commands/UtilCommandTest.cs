using MCSM.Ui.Repl.Commands;
using Xunit;
using MCSM.Ui.Test.Util;

namespace MCSM.Ui.Test.Repl.Commands
{
    public class HelpCommandTest
    {
        [Fact]
        public void When_ValidHelpCommand_Then_ShowHelp()
        {
            var writer = new TestTextWriter();
            var console = new TestConsole(writer, new TestTextReader());
            var lifecycle = new TestApplicationLifecycle();
            var repl = new Ui.Repl.Repl(console, new RootCommand(lifecycle, console));
            
            repl.ComputeInput("help");
        }

        [Fact]
        public void When_ValidVersionCommand_Then_ShowVersion()
        {
            //var repl = new Ui.Repl.Repl(new TestApplication());
            //repl.ComputeInput("version");
        }

        [Fact]
        public void When_ValidExitCommand_Then_Exit()
        {
            //var repl = new Ui.Repl.Repl(new TestApplication());
            //repl.ComputeInput("exit");
        }
    }
}