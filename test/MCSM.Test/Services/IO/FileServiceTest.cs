using System.IO;
using System.IO.Abstractions.TestingHelpers;
using MCSM.Services.IO;
using MCSM.Test.Util;
using Xunit;

namespace MCSM.Test.Services.IO
{
    public class FileServiceTest : IClassFixture<InitializeClassFixture>
    {
        [Fact]
        public void When_PathNew_Then_CreatePath()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);

            var path = fileService.Path();
            var path1 = fileService.Path("test1", path);
            var path2 = fileService.Path("test2", path, false);

            fileService.InitPath("./", path);

            Assert.True(fileSystem.Directory.Exists(path.AbsolutePath));
            Assert.True(fileSystem.Directory.Exists(path1.AbsolutePath));
            Assert.True(fileSystem.File.Exists(path2.AbsolutePath));
        }

        [Fact]
        public void When_PathResolved_Then_ReturnPath()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);

            var path = fileService.Path();

            var path1 = fileService.InitPath("./", path);
            var path2 = fileService.InitPath("./", path);

            Assert.Equal(path1, path2);
        }

        [Fact]
        public void When_PathExists_Delete()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);

            var path = fileService.Path();
            var path1 = fileService.Path("test1", path);
            var path2 = fileService.Path("test2", path, false);

            fileService.InitPath("./", path);

            fileService.Delete(path1);
            fileService.Delete(path2);

            Assert.False(fileSystem.Directory.Exists(path1.AbsolutePath));
            Assert.False(fileSystem.File.Exists(path2.AbsolutePath));
        }

        [Fact]
        public void When_PathExist_Then_True()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);

            var path = fileService.Path();
            var path1 = fileService.Path("test1", path);
            var path2 = fileService.Path("test2", path, false);

            fileService.InitPath("./", path);

            Assert.True(fileService.Exists(path1));
            Assert.True(fileService.Exists(path2));
        }

        [Fact]
        public void When_PathNotExist_Then_False()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);

            var path = fileService.Path();
            var path1 = fileService.Path("test1", path);
            var path2 = fileService.Path("test2", path, false);

            Assert.False(fileService.Exists(path1));
            Assert.False(fileService.Exists(path2));
        }

        [Fact]
        public void When_IsDirectory_GetFiles()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);

            var path = fileService.Path("./path");
            var path1 = fileService.Path("test1", path);
            var path2 = fileService.Path("test2", path, false);

            fileService.InitPath("./", path);

            Assert.Equal(new[] {path1.AbsolutePath, path2.AbsolutePath}, fileService.GetFiles(path));
        }

        [Fact]
        public void When_NotIsDirectory_GetNull()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);

            var path = fileService.Path("./path", isDirectory: false);

            Assert.Equal(fileService.GetFiles(path), new string[] { });
        }

        [Fact]
        public void When_FileExists_GetWriter()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);

            var path = fileService.Path("./path.txt", isDirectory: false);

            fileService.InitPath("./", path);

            var writer = fileService.FileWriter(path);
            writer.WriteLine("Hi");

            writer.Close();

            var reader = new StreamReader(fileSystem.File.Open(path.AbsolutePath, FileMode.OpenOrCreate));
            Assert.Equal("Hi", reader.ReadLine());
            reader.Close();
        }

        [Fact]
        public void When_FileExists_GetReader()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);

            var path = fileService.Path("./path.txt", isDirectory: false);

            fileService.InitPath("./", path);

            var writer = fileService.FileWriter(path);
            writer.WriteLine("Hi");
            writer.Close();

            var reader = fileService.FileReader(path);
            Assert.Equal("Hi", reader.ReadLine());
            reader.Close();
        }

        [Fact]
        public void When_IsDirectory_NotGetWriter()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);

            var path = fileService.Path("./path");

            fileService.InitPath("./", path);

            Assert.Null(fileService.FileWriter(path));
        }

        [Fact]
        public void When_IsDirectory_NotGetReader()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);

            var path = fileService.Path("./path");

            fileService.InitPath("./", path);

            Assert.Null(fileService.FileReader(path));
        }
    }
}