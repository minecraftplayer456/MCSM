using System.CommandLine;
using MCSM.Core.Test.Util;
using MCSM.Ui.Util;
using Xunit;
using Xunit.Abstractions;

namespace MCSM.Ui.Test.Repl
{
    /// <summary>
    ///     Class for all tests of the repl class
    /// </summary>
    public class ReplTest : BaseTest
    {
        public ReplTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        ///     If a none valid command is computed as input the returned result will be null
        /// </summary>
        [Fact]
        public void When_NoneValidCommand_Then_ReturnNull()
        {
            //set up repl with console
            var console = new Console();
            var repl = new Ui.Repl.Repl(console, new DummyCommand());

            //Assert if returned result is null
            Assert.Null(repl.ComputeInput("").Result);
        }

        /// <summary>
        ///     Dummy command that has no options and will no be executed
        /// </summary>
        public class DummyCommand : Command
        {
            public DummyCommand() : base("dummy", "dummy command")
            {
            }
        }
    }
}