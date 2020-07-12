using MCSM.Util.IO;

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
        public Workspace(string name)
        {
            Name = name;

            Path = new Path();
            WorldsPath = new Path("worlds", Path);
            ServersPath = new Path("servers", Path);
            JsonPath = new Path("workspace.json", Path, false);
        }
    }
}