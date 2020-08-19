using MCSM.Core.Test.Util;
using Xunit;
using Xunit.Abstractions;

namespace MCSM.Test
{
    /// <summary>
    ///     Test class for application instance tests
    /// </summary>
    public class ApplicationTest : BaseTest
    {
        public ApplicationTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        ///     Test if programme start and stops
        /// </summary>
        [Fact]
        public void TestApplicationLifecycle()
        {
            //TODO Make new
            var application = new Application();

            application.Start(new[] {"--debug", "--no-repl"});
            application.Stop();
        }
    }
}