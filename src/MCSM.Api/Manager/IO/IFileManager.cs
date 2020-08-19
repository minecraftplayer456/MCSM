using System.Collections.Generic;
using System.IO;

namespace MCSM.Api.Manager.IO
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
        public IPath Path(string relativePath = "", IPath parent = null, bool isDirectory = true,
            bool recursiveInit = true);

        /// <summary>
        ///     Computes the absolute path of a path from rootPath and relativePath
        /// </summary>
        /// <param name="rootPath">path that will be added to the front</param>
        /// <param name="path">path to compute absolute path</param>
        /// <returns>path with computed absolute path</returns>
        public IPath ComputeAbsolute(string rootPath, IPath path);

        /// <summary>
        ///     Initializes a path. That means to compute absolute path, create directory or file and initialize all child paths
        ///     that are recursiveInit true
        /// </summary>
        /// <param name="rootPath">path that will be added to the front</param>
        /// <param name="path">path to initialize</param>
        /// <returns>initialized path</returns>
        public IPath InitPath(string rootPath, IPath path);

        /// <summary>
        ///     Deletes file or directory with entries
        /// </summary>
        /// <param name="path">path to file or directory to delete</param>
        public void Delete(IPath path);

        /// <summary>
        ///     True if file or directory exists
        /// </summary>
        /// <param name="path">path to directory or file for checking</param>
        /// <returns>True if file or directory exists</returns>
        public bool Exists(IPath path);

        /// <summary>
        ///     Get all entries of a directory. Path must be a directory
        /// </summary>
        /// <param name="path">path to a directory with entries</param>
        /// <param name="searchPattern">search pattern for finding</param>
        /// <returns>entries of directory</returns>
        public IEnumerable<string> GetFiles(IPath path, string searchPattern = "*");

        /// <summary>
        ///     Creates new stream writer to write to a file. Path must be a file or the return will be null.
        /// </summary>
        /// <param name="path">path to file to be written</param>
        /// <returns>stream writer for file</returns>
        public StreamWriter FileWriter(IPath path);

        /// <summary>
        ///     Creates new stream reader to read a file. Path must be a file or the return will be null.
        /// </summary>
        /// <param name="path">path to file to be read</param>
        /// <returns>stream reader for file</returns>
        public StreamReader FileReader(IPath path);
    }

    /// <summary>
    ///     This interface contains information about a path on the filesystem.
    ///     It contains absolut path, relative path, children, parent and if the path is a directory or a file.
    ///     When a path is called with initPath in IFileManager the directory or file will be created and children with
    ///     parameter RecursiveInit will be initialized too.
    /// </summary>
    public interface IPath
    {
        /// <summary>
        ///     The children of path which are resolved
        /// </summary>
        public List<IPath> Children { get; }

        /// <summary>
        ///     If true path points to a directory
        /// </summary>
        public bool IsDirectory { get; }

        /// <summary>
        ///     Parent of path
        /// </summary>
        public IPath Parent { get; }

        /// <summary>
        ///     If true path will be initialized by its parent
        /// </summary>
        public bool RecursiveInit { get; }

        /// <summary>
        ///     relative path to file or directory
        /// </summary>
        public string RelativePath { get; }

        /// <summary>
        ///     absolute path to file or directory
        /// </summary>
        public string AbsolutePath { get; set; }
    }
}