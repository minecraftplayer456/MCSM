using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog;

namespace MCSM.Util.IO
{
    /// <summary>
    ///     Class that represents a relative path with parents and children.
    ///     When initializing the absolute path will be generated.
    /// </summary>
    public class Path
    {
        private readonly List<Path> _children;
        public readonly bool IsDirectory;
        public readonly Path Parent;
        public readonly bool RecursiveInit;
        public readonly string RelativePath;

        /// <summary>
        ///     Creates new path
        /// </summary>
        /// <param name="relativePath">relative Path</param>
        /// <param name="parent">Parent that will be resolved with relative path</param>
        /// <param name="isDirectory">True if path is an directory</param>
        /// <param name="recursiveInit">When true it will be only initialized if initialize is called on this object.</param>
        public Path(string relativePath = "", Path parent = null, bool isDirectory = true, bool recursiveInit = true)
        {
            IsDirectory = isDirectory;
            RecursiveInit = recursiveInit;
            RelativePath = relativePath;

            _children = new List<Path>();
            Parent = parent;
            Parent?._children.Add(this);
        }

        public IEnumerable<Path> Children => _children.ToArray();

        public string AbsolutePath { get; private set; }

        /// <summary>
        ///     Create absolut path for this and all its children if recursive init is true
        /// </summary>
        /// <param name="rootPath">path were the relative path will be calculated</param>
        /// <returns>the path itself</returns>
        public Path Initialize(string rootPath)
        {
            if (AbsolutePath != null)
            {
                Log.Warning("Path {absolutePath} is already resolved", AbsolutePath);
                return this;
            }

            AbsolutePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(rootPath, RelativePath));

            Log.Debug("Resolve path {relativePath} to {absolutePath}", RelativePath, AbsolutePath);

            if (IsDirectory)
            {
                if (!Directory.Exists(AbsolutePath))
                {
                    Directory.CreateDirectory(AbsolutePath);
                    Log.Verbose("Created directory: {absolutePath}", AbsolutePath);
                }
            }
            else
            {
                if (!File.Exists(AbsolutePath))
                {
                    File.Create(AbsolutePath).Close();
                    Log.Verbose("Created File: {absolutePath}", AbsolutePath);
                }
            }

            foreach (var child in _children.Where(child => child.RecursiveInit))
                child.Initialize(AbsolutePath);

            return this;
        }
    }
}