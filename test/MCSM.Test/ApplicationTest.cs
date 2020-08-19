using MCSM.Core.Test.Util;
using MCSM.Ui.Test.Util;
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
            //Set up application with mock console
            var reader = new TestTextReader(new[] {"exit"});
            var console = new TestConsole(new TestTextWriter(), reader);
            var application = new Application(console);

            application.Start(new string[] { });
        }
    }
}