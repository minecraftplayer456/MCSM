using System.IO;
using MCSM.Util;
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

            var path = new Path();
            var path1 = new Path("test1", path);
            var path2 = new Path("test2.txt", path, false);

            path.Initialize(WorkspacePath);

            Assert.True(Directory.Exists(path.AbsolutePath));
            foreach (var pathChild in path.Children)
                Assert.True(pathChild.IsDirectory
                    ? Directory.Exists(pathChild.AbsolutePath)
                    : File.Exists(pathChild.AbsolutePath));
        }
    }
}