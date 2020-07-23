using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using Serilog;

namespace MCSM.Core.Manager.IO
{
    /// <summary>
    ///     Manager to communicate with the file system
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        ///     Creates a new path. It will not be initialized
        /// </summary>
        /// <param name="relativePath">relative path of path. This will be used with a root path to compute an absolute path</param>
        /// <param name="parent">the parent path that will be added to the front</param>
        /// <param name="isDirectory">
        ///     if true the path will be a directory and create one. Otherwise it would be a file and create
        ///     one
        /// </param>
        /// <param name="recursiveInit">if true the path will be initialized by its parent</param>
        /// <returns>new path instance</returns>
        public Path Path(string relativePath = "", Path parent = null, bool isDirectory = true,
            bool recursiveInit = true);

        /// <summary>
        ///     Computes the absolute path of a path from rootPath and relativePath
        /// </summary>
        /// <param name="rootPath">path that will be added to the front</param>
        /// <param name="path">path to compute absolute path</param>
        /// <returns>path with computed absolute path</returns>
        public Path ComputeAbsolute(string rootPath, Path path);

        /// <summary>
        ///     Initializes a path. That means to compute absolute path, create directory or file and initialize all child paths
        ///     that are recursiveInit true
        /// </summary>
        /// <param name="rootPath">path that will be added to the front</param>
        /// <param name="path">path to initialize</param>
        /// <returns>initialized path</returns>
        public Path InitPath(string rootPath, Path path);

        /// <summary>
        ///     Deletes file or directory with entries
        /// </summary>
        /// <param name="path">path to file or directory to delete</param>
        public void Delete(Path path);

        /// <summary>
        ///     True if file or directory exists
        /// </summary>
        /// <param name="path">path to directory or file for checking</param>
        /// <returns>True if file or directory exists</returns>
        public bool Exists(Path path);

        /// <summary>
        ///     Get all entries of a directory. Path must be a directory
        /// </summary>
        /// <param name="path">path to a directory with entries</param>
        /// <param name="searchPattern">search pattern for finding</param>
        /// <returns>entries of directory</returns>
        public IEnumerable<string> GetFiles(Path path, string searchPattern = "*");

        /// <summary>
        ///     Creates new stream writer to write to a file. Path must be a file or the return will be null.
        /// </summary>
        /// <param name="path">path to file to be written</param>
        /// <returns>stream writer for file</returns>
        public StreamWriter FileWriter(Path path);

        /// <summary>
        ///     Creates new stream reader to read a file. Path must be a file or the return will be null.
        /// </summary>
        /// <param name="path">path to file to be read</param>
        /// <returns>stream reader for file</returns>
        public StreamReader FileReader(Path path);
    }

    public class FileManager : IFileManager
    {
        private readonly IFileSystem _fs;
        private readonly ILogger _log;

        public FileManager(IFileSystem fs)
        {
            _log = Log.ForContext<FileManager>();
            _fs = fs;
        }

        #region Path

        public Path Path(string relativePath = "", Path parent = null, bool isDirectory = true,
            bool recursiveInit = true)
        {
            var path = new Path(relativePath, parent, isDirectory, recursiveInit);
            parent?.Children.Add(path);
            return path;
        }

        public Path ComputeAbsolute(string rootPath, Path path)
        {
            if (path.AbsolutePath != null)
            {
                _log.Warning("Path {absolutePath} is already computed", path.AbsolutePath);
                return path;
            }

            var absolutePath = _fs.Path.GetFullPath(
                _fs.Path.Combine(rootPath, path.RelativePath));

            _log.Debug("Resolve path {relativePath} to {absolutePath}", path.RelativePath, absolutePath);

            path.AbsolutePath = absolutePath;

            return path;
        }

        public Path InitPath(string rootPath, Path path)
        {
            path = ComputeAbsolute(rootPath, path);

            if (path.IsDirectory)
            {
                if (!_fs.Directory.Exists(path.AbsolutePath))
                {
                    _fs.Directory.CreateDirectory(path.AbsolutePath);
                    _log.Verbose("Created directory at {absolutePath}", path.AbsolutePath);
                }
            }
            else
            {
                if (!_fs.File.Exists(path.AbsolutePath))
                {
                    _fs.File.Create(path.AbsolutePath).Close();
                    _log.Verbose("Created file at {absolutePath}", path.AbsolutePath);
                }
            }

            foreach (var child in path.Children.Where(child => child.RecursiveInit)) InitPath(path.AbsolutePath, child);

            return path;
        }

        #endregion

        #region File

        public void Delete(Path path)
        {
            if (path.IsDirectory)
            {
                _log.Verbose("Deleting directory {absolutePath}", path.AbsolutePath);
                _fs.Directory.Delete(path.AbsolutePath);
            }
            else
            {
                _log.Verbose("Deleting file {absolutePath}", path.AbsolutePath);
                _fs.File.Delete(path.AbsolutePath);
            }
        }

        public bool Exists(Path path)
        {
            return path.IsDirectory ? _fs.Directory.Exists(path.AbsolutePath) : _fs.File.Exists(path.AbsolutePath);
        }

        public IEnumerable<string> GetFiles(Path path, string searchPattern = "*")
        {
            if (path.IsDirectory) return _fs.Directory.GetFileSystemEntries(path.AbsolutePath, searchPattern);

            _log.Warning("Path {absolutePath} is not a directory. Can not get files", path.AbsolutePath);
            return new string[] { };
        }

        #endregion

        #region ReadWrite

        public StreamWriter FileWriter(Path path)
        {
            if (!path.IsDirectory) return new StreamWriter(_fs.File.OpenWrite(path.AbsolutePath));

            _log.Warning("Path {absolutePath} is not a file. It can not be written", path.AbsolutePath);
            return null;
        }

        public StreamReader FileReader(Path path)
        {
            if (!path.IsDirectory) return new StreamReader(_fs.File.OpenRead(path.AbsolutePath));

            _log.Warning("Path {absolutePath} is not a file. It can not be read", path.AbsolutePath);
            return null;
        }

        #endregion
    }

    /// <summary>
    ///     Storage class for path information
    /// </summary>
    public class Path
    {
        internal Path(string relativePath, Path parent, bool isDirectory, bool recursiveInit)
        {
            RelativePath = relativePath;
            Parent = parent;
            IsDirectory = isDirectory;
            RecursiveInit = recursiveInit;
            Children = new List<Path>();
        }

        public List<Path> Children { get; }
        public bool IsDirectory { get; }
        public Path Parent { get; }
        public bool RecursiveInit { get; }
        public string RelativePath { get; }

        public string AbsolutePath { get; internal set; }
    }
}