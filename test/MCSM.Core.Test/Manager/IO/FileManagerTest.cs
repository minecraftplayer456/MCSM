using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using MCSM.Core.Manager.IO;
using MCSM.Core.Test.Util;
using Xunit;

namespace MCSM.Core.Test.Manager.IO
{
    public class FileManagerTest : IClassFixture<InitializationClassFixture>
    {
        [Fact]
        public void When_PathsNotExist_Then_Create()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            var path = fileManager.Path();
            var directory = fileManager.Path("directory", path);
            var file = fileManager.Path("file.txt", path, false);

            fileManager.InitPath("./", path);

            Assert.True(fileSystem.Directory.Exists(fileSystem.Path.GetFullPath("./")));
            Assert.True(fileSystem.Directory.Exists(fileSystem.Path.GetFullPath("./directory")));
            Assert.True(fileSystem.File.Exists(fileSystem.Path.GetFullPath("./file.txt")));
        }

        [Fact]
        public void When_NotRecursiveInit_Then_NotInitialize()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            var path = fileManager.Path();
            var directory = fileManager.Path("directory", path, recursiveInit: false);
            var file = fileManager.Path("file.txt", path, false, false);

            fileManager.InitPath("./", path);

            Assert.True(fileSystem.Directory.Exists(fileSystem.Path.GetFullPath("./")));
            Assert.False(fileSystem.Directory.Exists(fileSystem.Path.GetFullPath("./directory")));
            Assert.False(fileSystem.File.Exists(fileSystem.Path.GetFullPath("./file.txt")));
        }

        [Fact]
        public void When_AbsolutePathAlreadyComputed_Then_Return()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            var path = fileManager.Path();

            fileManager.ComputeAbsolute("./", path);

            var absolutePath = path.AbsolutePath;

            Assert.Equal(fileManager.ComputeAbsolute("./", path).AbsolutePath, absolutePath);
        }

        [Fact]
        public void When_DirectoryExists_Then_Delete()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.Directory.CreateDirectory("./directory");

            var path = fileManager.Path("directory");

            fileManager.ComputeAbsolute("./", path);

            fileManager.Delete(path);

            Assert.False(fileSystem.Directory.Exists("./directory"));
        }

        [Fact]
        public void When_FileExists_Then_Delete()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.File.Create("./file.txt").Close();

            var path = fileManager.Path("file.txt", isDirectory: false);

            fileManager.ComputeAbsolute("./", path);

            fileManager.Delete(path);

            Assert.False(fileSystem.File.Exists("./file.txt"));
        }

        [Fact]
        public void When_PathExists_Then_True()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.Directory.CreateDirectory("./directory");
            fileSystem.File.Create("./file.txt").Close();

            var directory = fileManager.Path("directory");
            var file = fileManager.Path("file.txt", isDirectory: false);

            fileManager.ComputeAbsolute("./", directory);
            fileManager.ComputeAbsolute("./", file);

            Assert.True(fileManager.Exists(directory));
            Assert.True(fileManager.Exists(file));
        }

        [Fact]
        public void When_PathNotExist_Then_False()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            var directory = fileManager.Path("directory");
            var file = fileManager.Path("file.txt", isDirectory: false);

            fileManager.ComputeAbsolute("./", directory);
            fileManager.ComputeAbsolute("./", file);

            Assert.False(fileManager.Exists(directory));
            Assert.False(fileManager.Exists(file));
        }

        [Fact]
        public void When_DirectoryExists_Then_GetFiles()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.Directory.CreateDirectory("./directory");
            fileSystem.File.Create("./directory/file.txt").Close();

            var directory = fileManager.Path("directory");
            var file = fileManager.Path("file.txt", directory, false);

            fileManager.InitPath("./", directory);

            Assert.EndsWith("file.txt", fileManager.GetFiles(directory).Last());
        }

        [Fact]
        public void When_DirectoryNotExist_Then_GetNotFiles()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.File.Create("./file.txt").Close();

            var file = fileManager.Path("file.txt", isDirectory: false);

            fileManager.ComputeAbsolute("./", file);

            Assert.Empty(fileManager.GetFiles(file));
        }

        [Fact]
        public void When_FileExists_Then_CreateWriter()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.File.Create("./file.txt").Close();

            var file = fileManager.Path("file.txt", isDirectory: false);

            fileManager.ComputeAbsolute("./", file);

            var writer = fileManager.FileWriter(file);
            writer.Write("Hello World!");
            writer.Close();

            var reader = new StreamReader(fileSystem.File.OpenRead("./file.txt"));
            Assert.Equal("Hello World!", reader.ReadLine());
            reader.Close();
        }

        [Fact]
        public void When_FileIsDirectory_Then_NotReturnWriter()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.Directory.CreateDirectory("./directory");

            var directory = fileManager.Path("directory");

            fileManager.ComputeAbsolute("./", directory);

            Assert.Null(fileManager.FileWriter(directory));
        }

        [Fact]
        public void When_FileExists_Then_CreateReader()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.File.Create("./file.txt").Close();

            var file = fileManager.Path("file.txt", isDirectory: false);

            fileManager.ComputeAbsolute("./", file);

            var writer = new StreamWriter(fileSystem.File.OpenWrite("./file.txt"));
            writer.Write("Hello World!");
            writer.Close();

            var reader = fileManager.FileReader(file);
            Assert.Equal("Hello World!", reader.ReadLine());
            reader.Close();
        }

        [Fact]
        public void When_FileIsDirectory_Then_NotReturnReader()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.Directory.CreateDirectory("./directory");

            var directory = fileManager.Path("directory");

            fileManager.ComputeAbsolute("./", directory);

            Assert.Null(fileManager.FileReader(directory));
        }
    }
}