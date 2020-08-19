using System.IO.Abstractions.TestingHelpers;
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
            var path = new Path();
            new Path("directory", path);
            new Path("file.txt", path, false);

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
            var path = new Path();
            new Path("directory", path, recursiveInit: false);
            new Path("file.txt", path, false, false);

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
            var path = new Path();

            // Compute absolute path for path
            fileManager.ComputeAbsolute("/", path);

            // Assert if the first and second returned path are the same
            Assert.Equal(fileManager.ComputeAbsolute("/", path).AbsolutePath.GetHashCode(),
                path.AbsolutePath.GetHashCode());
        }

        /// <summary>
        ///     If path exist then it will be deleted
        /// </summary>
        [Fact]
        public void When_PathExists_Then_Delete()
        {
            //Set up fileManager with mock file system
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            //Create directory and file
            fileSystem.Directory.CreateDirectory("/directory");
            fileSystem.File.Create("/file.txt").Close();

            //Create path for directory and file
            var directory = new Path("/directory");
            var file = new Path("file.txt", isDirectory: false);

            //Initialize path and directory
            fileManager.InitPath("/", directory);
            fileManager.InitPath("/", file);

            //Delete directory and file
            fileManager.Delete(directory);
            fileManager.Delete(file);

            // Assert that directory and file is deleted
            Assert.False(fileSystem.Directory.Exists("/directory"));
            Assert.False(fileSystem.File.Exists("/file.txt"));
        }

        /// <summary>
        ///     If valid directory exists then you could get the children files from it.
        /// </summary>
        [Fact]
        public void When_ValidDirectory_Then_GetFiles()
        {
            //Set up fileManager with mock file system
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            //Create directory and child file
            fileSystem.Directory.CreateDirectory("/directory");
            fileSystem.File.Create("/directory/file.txt").Close();

            //Create path for directory
            var directory = new Path("directory");
            var file = new Path("file.txt", directory);

            //Initialize directory
            fileManager.InitPath("/", directory);

            //Get files from directory
            var files = fileManager.GetFiles(directory);

            //Assert that file is included in directory
            Assert.Contains(file.AbsolutePath, files);
        }

        /// <summary>
        ///     If valid file exists then not get children files
        /// </summary>
        [Fact]
        public void When_File_Then_NotGetFiles()
        {
            //Set up fileManager with mock file system
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            //Create file
            fileSystem.File.Create("/file.txt").Close();

            //Create path for file
            var file = new Path("file.txt", isDirectory: false);

            //Initialize file
            fileManager.InitPath("/", file);

            //Get files from file
            var files = fileManager.GetFiles(file);

            //Assert that files are empty
            Assert.Null(files);
        }

        /// <summary>
        ///     If valid file is present you can create a text reader/writer
        /// </summary>
        [Fact]
        public void When_ValidFile_Then_CreateReaderWriter()
        {
            //Set up fileManager with mock file system
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            //Create file on filesystem
            fileSystem.File.Create("/file.txt").Close();

            //Create path for file
            var file = new Path("file.txt", isDirectory: false);

            //Initialize file
            fileManager.InitPath("/", file);

            //Create writer
            var writer = fileManager.FileWriter(file);

            //Assert writer is not null
            Assert.NotNull(writer);

            //Write to file
            writer.WriteLine("Hello");
            writer.Close();

            //Assert content was written
            Assert.Contains("Hello", fileSystem.GetFile("/file.txt").TextContents);

            //Create reader
            var reader = fileManager.FileReader(file);

            //Assert reader is not null
            Assert.NotNull(reader);

            //Read from file
            var read = reader.ReadLine();
            reader.Close();

            //Assert read content
            Assert.Equal("Hello", read);
        }

        /// <summary>
        ///     If valid directory is present then no reader/writer will be created
        /// </summary>
        [Fact]
        public void When_Directory_Then_NotCreateReaderWriter()
        {
            //Set up fileManager with mock file system
            var fileSystem = new MockFileSystem();
            var fileManager = new FileManager(fileSystem);

            //Create file on filesystem
            fileSystem.Directory.CreateDirectory("/directory");

            //directory path for file
            var directory = new Path("directory");

            //Initialize file
            fileManager.InitPath("/", directory);

            //Create writer
            var writer = fileManager.FileWriter(directory);
            var reader = fileManager.FileReader(directory);

            //Assert writer/reader is null
            Assert.Null(writer);
            Assert.Null(reader);
        }
    }
}