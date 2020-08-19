using MCSM.Api.Util;
using MCSM.Core.Test.Util;
using MCSM.Ui.Repl.Commands;
using MCSM.Ui.Test.Util;
using Xunit;
using Xunit.Abstractions;

namespace MCSM.Ui.Test.Repl.Commands
{
    /// <summary>
    ///     Test class for all utility repl commands
    /// </summary>
    public class UtilCommandTest : BaseTest
    {
        public UtilCommandTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        ///     If a valid help command is computed the help menu will be printed to console
        /// </summary>
        [Fact]
        public void When_ValidHelpCommand_Then_ShowHelp()
        {
            // Set up repl with mock console
            var writer = new TestTextWriter();
            var console = new TestConsole(writer, new TestTextReader());
            var repl = new Ui.Repl.Repl(console, new RootCommand(new TestApplicationLifecycle(), console));

            // Compute help input
            repl.ComputeInput("help");

            // Assert that "Usage:", "help", "Shows information about all commands" is included in res
            var content = writer.Content;
            Assert.Contains("Usage:", content);
            Assert.Contains("h, help", content);
            Assert.Contains("Shows information about all commands", content);
        }

        /// <summary>
        ///     When a valid version command is computed the assembly version will be printed to consolep
        /// </summary>
        [Fact]
        public void When_ValidVersionCommand_Then_ShowVersion()
        {
            // Set up repl with mock console
            var writer = new TestTextWriter();
            var console = new TestConsole(writer, new TestTextReader());
            var repl = new Ui.Repl.Repl(console, new RootCommand(new TestApplicationLifecycle(), console));

            //Compute version input
            repl.ComputeInput("version");

            // Assert if correct version is printed
            var content = writer.Content;
            Assert.Contains(Constants.McsmVersion, content);
        }

        /// <summary>
        ///     When valid exit command is computed the stop method in application will be executed
        /// </summary>
        [Fact]
        public void When_ValidExitCommand_Then_Exit()
        {
            //Set up mock console
            var writer = new TestTextWriter();
            var console = new TestConsole(writer, new TestTextReader());

            //Set up mock application life cycle
            var stopped = false;
            var testLifecycle = new TestApplicationLifecycle(stopDel: () => { stopped = true; });

            //Set up repl
            var repl = new Ui.Repl.Repl(console, new RootCommand(testLifecycle, console));

            //Compute exit input
            repl.ComputeInput("exit");

            //Assert if stop method was called
            Assert.True(stopped);
        }
    }
}