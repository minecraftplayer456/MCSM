using MCSM.Core.Test.Util;
using Xunit;

namespace MCSM.Ui.Test.Cli
{
    public class CliTest : IClassFixture<InitializationClassFixture>
    {
        [Fact]
        public void When_DebugFlag_Then_VerboseLogging()
        {
            var cli = new Ui.Cli.Cli();
            var result = cli.Parse(new[] {"--debug"});
            Assert.True(result.Debug);
        }
    }
}