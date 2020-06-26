using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog;

namespace MCSM.Util.IO
{
    /// <summary>
    ///     Class that holds schematics for path class
    /// </summary>
    public sealed class Paths
    {
        public static readonly Path Workspace = new Path();
        public static readonly Path Servers = new Path("Servers", Workspace);
        public static readonly Path Worlds = new Path("Worlds", Workspace);

        public static readonly Path WorkspaceJson = new Path("workspace.json", Workspace, false);
    }

    /// <summary>
    ///     Class that helps organizing paths
    /// </summary>
    public class Path
    {
        private readonly List<Path> _children;

        public readonly bool IsDirectory;
        public readonly bool LazyInit;
        public readonly Path Parent;
        public readonly string RelativePath;

        /// <summary>
        ///     Create new path
        /// </summary>
        /// <param name="relativePath">relative Path</param>
        /// <param name="parent">Parent that will be resolved with relative path</param>
        /// <param name="isDirectory">True if path is an directory</param>
        /// <param name="lazyInit">When true it will be only initialized if lazyInit is called.</param>
        public Path(string relativePath = "", Path parent = null, bool isDirectory = true, bool lazyInit = false)
        {
            RelativePath = relativePath;
            IsDirectory = isDirectory;
            LazyInit = lazyInit;

            _children = new List<Path>();
            parent?._children.Add(this);
            Parent = parent;
        }

        public string AbsolutePath { get; private set; }
        public Path[] Children => _children.ToArray();

        /// <summary>
        ///     Compute absolute path and create folder/file
        /// </summary>
        /// <param name="rootPath">Path where created</param>
        /// <returns>this instance of path</returns>
        public Path Initialize(string rootPath)
        {
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

            foreach (var child in _children.Where(child => !child.LazyInit))
                child.Initialize(AbsolutePath);

            return this;
        }

        /// <summary>
        ///     This method is called if you init a path lazily.
        ///     This method can only be called if parent is initialized
        /// </summary>
        /// <returns>instance of path</returns>
        public Path InitializeLazy()
        {
            if (!LazyInit) return null;
            return Parent.AbsolutePath != null ? Initialize(Parent.AbsolutePath) : null;
        }
    }
}