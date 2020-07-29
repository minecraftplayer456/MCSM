using Xunit;

namespace MCSM.Test
{
    public class ApplicationTest
    {
        [Fact]
        public void TestApplicationLifecycle()
        {
            var application = new Application();
            
            application.Start(new []{"--debug", "--no-repl"});
            application.Stop();
        }
    }
}