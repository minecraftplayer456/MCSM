using MCSM.Core.Test.Util;
using Xunit;
using Xunit.Abstractions;

namespace MCSM.Ui.Test.Cli
{
    /// <summary>
    ///     Test class for all tests related with ICli class
    /// </summary>
    public class CliTest : BaseTest
    {
        public CliTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        ///     If parsing --debug flag then debug will be true in result
        /// </summary>
        [Fact]
        public void When_DebugFlag_Then_VerboseLogging()
        {
            //Create new cli
            var cli = new Ui.Cli.Cli();

            //Parse --debug flag
            var result = cli.Parse(new[] {"--debug"});

            //Assert if debug is true
            Assert.True(result.Debug);
        }

        /// <summary>
        ///     If parsing --no-repl in result no-repl will be true
        /// </summary>
        [Fact]
        public void When_NoReplFlag_Then_NoRepl()
        {
            //Create new cli
            var cli = new Ui.Cli.Cli();

            //Parse --no-repl
            var result = cli.Parse(new[] {"--no-repl"});

            //Assert if no repl is true
            Assert.True(result.NoRepl);
        }
    }
}