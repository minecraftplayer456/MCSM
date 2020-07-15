using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using MCSM.Util;
using Serilog;

namespace MCSM.Services.IO
{
    /// <summary>
    ///     Service class to manipulate the file system
    /// </summary>
    public class FileService : LazyAble<FileService>
    {
        private readonly IFileSystem _fs;

        public FileService(IFileSystem fs)
        {
            _fs = fs;
        }

        public FileService() : this(new FileSystem())
        {
        }

        #region Path

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
        /// <returns></returns>
        public Path Path(string relativePath = "", Path parent = null, bool isDirectory = true,
            bool recursiveInit = true)
        {
            var path = new Path(relativePath, parent, isDirectory, recursiveInit);
            parent?.Children.Add(path);
            return path;
        }

        /// <summary>
        ///     Computes the absolute path of a path from rootPath and relativePath
        /// </summary>
        /// <param name="rootPath">path that will be added to the front</param>
        /// <param name="path">path to compute absolute path</param>
        /// <returns></returns>
        public Path ComputeAbsolute(string rootPath, Path path)
        {
            if (path.AbsolutePath != null)
            {
                Log.Warning("Path {absolutePath} is already resolved", path.AbsolutePath);
                return path;
            }

            var absolutePath = _fs.Path.GetFullPath(
                _fs.Path.Combine(rootPath, path.RelativePath));

            Log.Debug("Resolve path {relativePath} to {absolutePath}", path.RelativePath, absolutePath);

            path.AbsolutePath = absolutePath;

            return path;
        }

        /// <summary>
        ///     Initializes a path. That means to compute absolute path, create directory or file and initialize all child paths
        ///     that are recursiveInit true
        /// </summary>
        /// <param name="rootPath">path that will be added to the front</param>
        /// <param name="path">path to initialize</param>
        /// <returns></returns>
        public Path InitPath(string rootPath, Path path)
        {
            path = ComputeAbsolute(rootPath, path);

            if (path.IsDirectory)
            {
                if (!_fs.Directory.Exists(path.AbsolutePath))
                {
                    _fs.Directory.CreateDirectory(path.AbsolutePath);
                    Log.Verbose("Created directory at {absolutePath}", path.AbsolutePath);
                }
            }
            else
            {
                if (!_fs.File.Exists(path.AbsolutePath))
                {
                    _fs.File.Create(path.AbsolutePath).Close();
                    Log.Verbose("Created file at {absolutePath}", path.AbsolutePath);
                }
            }

            foreach (var child in path.Children.Where(child => child.RecursiveInit)) InitPath(path.AbsolutePath, child);

            return path;
        }

        #endregion

        #region File

        /// <summary>
        ///     Deletes a directory or file
        /// </summary>
        /// <param name="path">path to directory or file</param>
        public void Delete(Path path)
        {
            if (path.IsDirectory)
                _fs.Directory.Delete(path.AbsolutePath, true);
            else
                _fs.File.Delete(path.AbsolutePath);
        }

        /// <summary>
        ///     True if directory or file exists
        /// </summary>
        /// <param name="path">path to directory or file</param>
        /// <returns></returns>
        public bool Exists(Path path)
        {
            return path.IsDirectory ? _fs.Directory.Exists(path.AbsolutePath) : _fs.File.Exists(path.AbsolutePath);
        }

        /// <summary>
        ///     Get all child files and folders from a directory. Path must be a directory
        /// </summary>
        /// <param name="path">Path to directory to get child files</param>
        /// <param name="searchPattern">search pattern for finding</param>
        /// <returns></returns>
        public string[] GetFiles(Path path, string searchPattern = "*")
        {
            if (path.IsDirectory) return _fs.Directory.GetFileSystemEntries(path.AbsolutePath, searchPattern);

            Log.Warning("Path {absolutePath} is not a directory. Can not get files", path.AbsolutePath);
            return new string[] { };
        }

        #endregion

        #region FileReadWrite

        /// <summary>
        ///     Creates new stream file writer to write to file. Path must be a file
        /// </summary>
        /// <param name="path">path to file to be written</param>
        /// <returns></returns>
        public StreamWriter FileWriter(Path path)
        {
            if (!path.IsDirectory) return new StreamWriter(_fs.File.Open(path.AbsolutePath, FileMode.Open));

            Log.Warning("Path {absolutePath} must be a file. It can not be written", path.AbsolutePath);
            return null;
        }

        /// <summary>
        ///     Creates new stream file reader to read a file. Path must be a file
        /// </summary>
        /// <param name="path">path to file to be read</param>
        /// <returns></returns>
        public StreamReader FileReader(Path path)
        {
            if (!path.IsDirectory) return new StreamReader(_fs.File.Open(path.AbsolutePath, FileMode.Open));

            Log.Warning("Path {absolutePath} must be a file. It can not be read", path.AbsolutePath);
            return null;
        }

        #endregion
    }

    public class Path
    {
        public readonly List<Path> Children;
        public readonly bool IsDirectory;
        public readonly Path Parent;
        public readonly bool RecursiveInit;
        public readonly string RelativePath;

        internal Path(string relativePath, Path parent, bool isDirectory, bool recursiveInit)
        {
            RelativePath = relativePath;
            Parent = parent;
            IsDirectory = isDirectory;
            RecursiveInit = recursiveInit;

            Children = new List<Path>();
        }

        public string AbsolutePath { get; internal set; }
    }
}