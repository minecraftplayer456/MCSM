using Xunit;
using MCSM;
using MCSM.Api;
using MCSM.Api.Manager.IO;
using MCSM.Api.Ui;

namespace MCSM.Ui.Test.Repl.Commands
{
    public class TestApplication : IApplication
    {
        public ILogManager LogManager { get; }
        public IFileManager FileManager { get; }
        public IRepl Repl { get; }

        public TestApplication()
        {
            Repl = new Ui.Repl.Repl(this);
        }
        
        public void Start(string[] args)
        {
            Repl.Run();
        }

        public void Stop()
        {
            Repl.Exit();
        }
    }
    
    public class HelpCommandTest
    {
        [Fact]
        public void When_ValidHelpCommand_Then_ShowHelp()
        {
            var repl = new Ui.Repl.Repl(new TestApplication());
            repl.ComputeInput("help");
        }

        [Fact]
        public void When_ValidVersionCommand_Then_ShowVersion()
        {
            var repl = new Ui.Repl.Repl(new TestApplication());
            repl.ComputeInput("version");
        }

        [Fact]
        public void When_ValidExitCommand_Then_Exit()
        {
            var repl = new Ui.Repl.Repl(new TestApplication());
            repl.ComputeInput("exit");
        }
    }
}