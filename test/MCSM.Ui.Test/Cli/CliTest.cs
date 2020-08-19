using MCSM.Core.Test.Util;
using Xunit;
using Xunit.Abstractions;

namespace MCSM.Ui.Test.Cli
{
    public class CliTest : BaseTest
    {
        public CliTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void When_DebugFlag_Then_VerboseLogging()
        {
            var cli = new Ui.Cli.Cli();
            var result = cli.Parse(new[] {"--debug"});
            Assert.True(result.Debug);
        }

        [Fact]
        public void When_NoReplFlag_Then_NoRepl()
        {
            var cli = new Ui.Cli.Cli();
            var result = cli.Parse(new[] {"--no-repl"});
            Assert.True(result.NoRepl);
        }
    }
}