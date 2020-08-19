using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using MCSM.Api.Manager.IO;
using Serilog;
using IPath = MCSM.Api.Manager.IO.IPath;

namespace MCSM.Core.Manager.IO
{
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

        public IPath Path(string relativePath = "", IPath parent = null, bool isDirectory = true,
            bool recursiveInit = true)
        {
            var path = new Path(relativePath, parent, isDirectory, recursiveInit);
            parent?.Children.Add(path);
            return path;
        }

        public IPath ComputeAbsolute(string rootPath, IPath path)
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

        public IPath InitPath(string rootPath, IPath path)
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

        //TODO Test with Exist if path exists
        public void Delete(IPath path)
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

        public bool Exists(IPath path)
        {
            return path.IsDirectory ? _fs.Directory.Exists(path.AbsolutePath) : _fs.File.Exists(path.AbsolutePath);
        }

        //TODO Test with Exist if path exists
        public IEnumerable<string> GetFiles(IPath path, string searchPattern = "*")
        {
            if (path.IsDirectory) return _fs.Directory.GetFileSystemEntries(path.AbsolutePath, searchPattern);

            _log.Warning("Path {absolutePath} is not a directory. Can not get files", path.AbsolutePath);
            return new string[] { };
        }

        #endregion

        #region ReadWrite

        public StreamWriter FileWriter(IPath path)
        {
            if (!path.IsDirectory) return new StreamWriter(_fs.File.OpenWrite(path.AbsolutePath));

            _log.Warning("Path {absolutePath} is not a file. It can not be written", path.AbsolutePath);
            return null;
        }

        public StreamReader FileReader(IPath path)
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
    public class Path : IPath
    {
        internal Path(string relativePath, IPath parent, bool isDirectory, bool recursiveInit)
        {
            RelativePath = relativePath;
            Parent = parent;
            IsDirectory = isDirectory;
            RecursiveInit = recursiveInit;
            Children = new List<IPath>();
        }

        public List<IPath> Children { get; }
        public bool IsDirectory { get; }
        public IPath Parent { get; }
        public bool RecursiveInit { get; }
        public string RelativePath { get; }

        public string AbsolutePath { get; set; }
    }
}