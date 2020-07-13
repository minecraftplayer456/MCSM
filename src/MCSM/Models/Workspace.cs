using MCSM.Services.IO;

namespace MCSM.Models
{
    /// <summary>
    ///     A workspace is a folder where all data like config, mods, servers, world are stored
    /// </summary>
    public class Workspace
    {
        public readonly Path JsonPath;
        public readonly string Name;

        public readonly Path Path;
        public readonly Path ServersPath;
        public readonly Path WorldsPath;

        /// <summary>
        ///     Creates new workspace model with given name
        /// </summary>
        /// <param name="name">name for workspace model</param>
        /// <param name="path">path to workspace</param>
        public Workspace(string name, Path path)
        {
            Name = name;

            Path = path;
            WorldsPath = FileService.Default.Path("worlds", Path);
            ServersPath = FileService.Default.Path("servers", Path);
            JsonPath = FileService.Default.Path("workspace.json", Path, false);
        }
    }
}