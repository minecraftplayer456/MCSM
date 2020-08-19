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
            _fs = fs;
            _log = Log.ForContext<FileManager>();
        }

        #region Path

        public IPath Path(string relativePath = "", IPath parent = null, bool isDirectory = true,
            bool recursiveInit = true)
        {
            //Creates new path
            var path = new Path(relativePath, parent, isDirectory, recursiveInit);

            //Add children to parent
            parent?.Children.Add(path);

            return path;
        }

        public IPath ComputeAbsolute(string rootPath, IPath path)
        {
            // Check if absolute path is already computed and return it
            if (path.AbsolutePath != null)
            {
                _log.Warning("Path {absolutePath} is already computed", path.AbsolutePath);
                return path;
            }

            // Combines root path with relative path and gets the full path (absolute) 
            var absolutePath = _fs.Path.GetFullPath(
                _fs.Path.Combine(rootPath, path.RelativePath));

            _log.Debug("Resolve path {relativePath} to {absolutePath}", path.RelativePath, absolutePath);

            path.AbsolutePath = absolutePath;

            return path;
        }

        public IPath InitPath(string rootPath, IPath path)
        {
            //Computes absolute path
            path = ComputeAbsolute(rootPath, path);

            // If path does not exist a new file or directory will be created
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

            // Every path where RecursiveInit is true will be initialized
            foreach (var child in path.Children.Where(child => child.RecursiveInit)) InitPath(path.AbsolutePath, child);

            return path;
        }

        #endregion

        #region File

        public void Delete(IPath path)
        {
            // Validate path
            if (!Validate(path)) return;

            //Delete file or directory
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

        public IEnumerable<string> GetFiles(IPath path, string searchPattern = "*")
        {
            //Validate path
            if (!Validate(path)) return null;

            //If path is a directory get the children paths
            if (path.IsDirectory) return _fs.Directory.GetFileSystemEntries(path.AbsolutePath, searchPattern);

            //If it's an file return null
            _log.Warning("Path {absolutePath} is not a directory. Can not get files", path.AbsolutePath);
            return null;
        }

        public bool Validate(IPath path)
        {
            // Test if path hasn't absolute path then it's not valid
            if (path.AbsolutePath != null)
            {
                // Test if path not exist then it's not valid
                if (Exists(path))
                {
                    _log.Verbose("Path {absolutePath} is valid", path.AbsolutePath);
                    return true;
                }

                _log.Warning("Path {absolutePath} does not exist", path.AbsolutePath);
                return false;
            }

            _log.Warning("Path {relativePath} is not valid: No absolute path was initialized", path.RelativePath);
            return false;
        }

        public bool Exists(IPath path)
        {
            // True if directory of file exists
            return path.IsDirectory ? _fs.Directory.Exists(path.AbsolutePath) : _fs.File.Exists(path.AbsolutePath);
        }

        #endregion

        #region ReadWrite

        public StreamWriter FileWriter(IPath path)
        {
            //Validate file
            if (!Validate(path)) return null;

            // If path is file then return stream writer
            if (!path.IsDirectory) return new StreamWriter(_fs.File.OpenWrite(path.AbsolutePath));

            //Else path is a directory = return null
            _log.Warning("Path {absolutePath} is not a file. It can not be written", path.AbsolutePath);
            return null;
        }

        public StreamReader FileReader(IPath path)
        {
            // Validate file
            if (!Validate(path)) return null;

            // If path is file then return stream reader
            if (!path.IsDirectory) return new StreamReader(_fs.File.OpenRead(path.AbsolutePath));

            //Else path is a directory = return null
            _log.Warning("Path {absolutePath} is not a file. It can not be read", path.AbsolutePath);
            return null;
        }

        #endregion
    }

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