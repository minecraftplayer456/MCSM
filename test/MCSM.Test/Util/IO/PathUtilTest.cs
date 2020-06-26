using System.IO;
using MCSM.Util;
using MCSM.Util.IO;
using Xunit;
using Path = MCSM.Util.IO.Path;

namespace MCSM.Test.Util.IO
{
    [Collection("IO")]
    public class PathUtilTest : IClassFixture<InitializeClassFixture>
    {
        private const string WorkspacePath = Constants.DefaultWorkspacePath;

        [Fact]
        public void When_NoFilesFound_Then_Create()
        {
            if (Directory.Exists(WorkspacePath)) Directory.Delete(WorkspacePath, true);

            var path = Paths.Workspace.Initialize(WorkspacePath);

            Assert.True(Directory.Exists(path.AbsolutePath));
            foreach (var pathChild in path.Children)
                Assert.True(pathChild.IsDirectory
                    ? Directory.Exists(pathChild.AbsolutePath)
                    : File.Exists(pathChild.AbsolutePath));
        }

        [Fact]
        public void When_ParentInitialized_Then_InitLazy()
        {
            if (Directory.Exists(WorkspacePath)) Directory.Delete(WorkspacePath, true);

            Paths.Workspace.Initialize(WorkspacePath);
            var lazyPath = new Path("Server1", Paths.Servers, lazyInit: true);

            lazyPath.InitializeLazy();

            Assert.True(Directory.Exists(lazyPath.AbsolutePath));
        }
    }
}