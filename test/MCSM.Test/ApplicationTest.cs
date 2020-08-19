using MCSM.Core.Test.Util;
using Xunit;
using Xunit.Abstractions;

namespace MCSM.Test
{
    public class ApplicationTest : BaseTest
    {
        public ApplicationTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void TestApplicationLifecycle()
        {
            var application = new Application();

            application.Start(new[] {"--debug", "--no-repl"});
            application.Stop();
        }
    }
}