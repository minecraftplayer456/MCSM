using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using MCSM.Core.Manager.IO;
using MCSM.Core.Test.Util;
using Xunit;
using Xunit.Abstractions;

namespace MCSM.Core.Test.Manager.IO
{
    /// <summary>
    ///     Class for tests of IFileManager
    /// </summary>
    public class FileManagerTest : BaseTest
    {
        public FileManagerTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        ///     If the file or directory at the path doesn't exist it will be created
        /// </summary>
        [Fact]
        public void When_PathsNotExist_Then_Create()
        {
            //Set up file manager and mock file system
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            // Creates one path for a directory and one for a file
            var path = fileManager.Path();
            fileManager.Path("directory", path);
            fileManager.Path("file.txt", path, false);

            // Initializes and create bothp
            fileManager.InitPath("/", path);

            // Assert if the directory and the file is created
            Assert.True(fileSystem.Directory.Exists(fileSystem.Path.GetFullPath("./")));
            Assert.True(fileSystem.Directory.Exists(fileSystem.Path.GetFullPath("./directory")));
            Assert.True(fileSystem.File.Exists(fileSystem.Path.GetFullPath("./file.txt")));
        }

        /// <summary>
        ///     If the recursive init is false and the parent is going to be created the child will not be created
        /// </summary>
        [Fact]
        public void When_NotRecursiveInit_Then_NotInitialize()
        {
            // Set up file manager with mock file system
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            // Creates path for directory and file with recursiveInit false
            var path = fileManager.Path();
            fileManager.Path("directory", path, recursiveInit: false);
            fileManager.Path("file.txt", path, false, false);

            // Initializes parent
            fileManager.InitPath("/", path);

            // Assert if parent is created and file, directory not
            Assert.True(fileSystem.Directory.Exists(fileSystem.Path.GetFullPath("/")));
            Assert.False(fileSystem.Directory.Exists(fileSystem.Path.GetFullPath("/directory")));
            Assert.False(fileSystem.File.Exists(fileSystem.Path.GetFullPath("/file.txt")));
        }

        /// <summary>
        ///     If path has already an absolute path computed and you computeAbsolute is called the old absolutePath will be
        ///     returned
        /// </summary>
        [Fact]
        public void When_AbsolutePathAlreadyComputed_Then_Return()
        {
            // Set up file manager with mock file system
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            // Creates a path
            var path = fileManager.Path();

            // Compute absolute path for path
            fileManager.ComputeAbsolute("/", path);

            // Assert if the first and second returned path are the same
            Assert.Equal(fileManager.ComputeAbsolute("/", path).AbsolutePath.GetHashCode(),
                path.AbsolutePath.GetHashCode());
        }

        /// <summary>
        ///     If a path exists and Delete is called the path will be deleted
        /// </summary>
        [Fact]
        public void When_PathExists_Then_Delete()
        {
            // Set up file manager and mock file system
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            // Creates a directory and a file
            fileSystem.Directory.CreateDirectory("/directory");
            fileSystem.File.Create("/file.txt").Close();

            // Creates both of them as path
            var directory = fileManager.Path("directory");
            var file = fileManager.Path("file.txt");

            // Computes absolute path
            fileManager.ComputeAbsolute("/", directory);
            fileManager.ComputeAbsolute("/", file);

            // Delete directory and file
            fileManager.Delete(directory);
            fileManager.Delete(file);

            // Assert that they are all deleted
            Assert.False(fileSystem.Directory.Exists("/directory"));
            Assert.False(fileSystem.File.Exists("/file.txt"));
        }

        //TODO Integrate in delete path tests
        [Fact]
        public void When_PathExists_Then_True()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.Directory.CreateDirectory("/directory");
            fileSystem.File.Create("/file.txt").Close();

            var directory = fileManager.Path("directory");
            var file = fileManager.Path("file.txt", isDirectory: false);

            fileManager.ComputeAbsolute("/", directory);
            fileManager.ComputeAbsolute("/", file);

            Assert.True(fileManager.Exists(directory));
            Assert.True(fileManager.Exists(file));
        }

        //TODO Integrate in delete path tests
        [Fact]
        public void When_PathNotExist_Then_False()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            var directory = fileManager.Path("directory");
            var file = fileManager.Path("file.txt", isDirectory: false);

            fileManager.ComputeAbsolute("/", directory);
            fileManager.ComputeAbsolute("/", file);

            Assert.False(fileManager.Exists(directory));
            Assert.False(fileManager.Exists(file));
        }

        [Fact]
        public void When_DirectoryExists_Then_GetFiles()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.Directory.CreateDirectory("/directory");
            fileSystem.File.Create("/directory/file.txt").Close();

            var directory = fileManager.Path("directory");
            fileManager.Path("file.txt", directory, false);

            fileManager.InitPath("/", directory);

            Assert.EndsWith("file.txt", fileManager.GetFiles(directory).Last());
        }

        [Fact]
        public void When_DirectoryNotExist_Then_GetNotFiles()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.File.Create("/file.txt").Close();

            var file = fileManager.Path("file.txt", isDirectory: false);

            fileManager.ComputeAbsolute("/", file);

            Assert.Empty(fileManager.GetFiles(file));
        }

        [Fact]
        public void When_FileExists_Then_CreateWriter()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.File.Create("/file.txt").Close();

            var file = fileManager.Path("file.txt", isDirectory: false);

            fileManager.ComputeAbsolute("/", file);

            var writer = fileManager.FileWriter(file);
            writer.Write("Hello World!");
            writer.Close();

            var reader = new StreamReader(fileSystem.File.OpenRead("/file.txt"));
            Assert.Equal("Hello World!", reader.ReadLine());
            reader.Close();
        }

        [Fact]
        public void When_FileIsDirectory_Then_NotReturnWriter()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.Directory.CreateDirectory("/directory");

            var directory = fileManager.Path("directory");

            fileManager.ComputeAbsolute("/", directory);

            Assert.Null(fileManager.FileWriter(directory));
        }

        [Fact]
        public void When_FileExists_Then_CreateReader()
        {
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            fileSystem.File.Create("/file.txt").Close();

            var file = fileManager.Path("file.txt", isDirectory: false);

            fileManager.ComputeAbsolute("/", file);

            var writer = new StreamWriter(fileSystem.File.OpenWrite("/file.txt"));
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

            fileSystem.Directory.CreateDirectory("/directory");

            var directory = fileManager.Path("directory");

            fileManager.ComputeAbsolute("/", directory);

            Assert.Null(fileManager.FileReader(directory));
        }
    }
}