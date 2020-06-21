namespace MCSM.Models
{
    /// <summary>
    ///     A workspace is a folder where all data like config, mods, servers, world are stored
    /// </summary>
    public class Workspace
    {
        /// <summary>
        ///     Creates new workspace model with given name
        /// </summary>
        /// <param name="name">name for workspace model</param>
        public Workspace(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Gets name of workspace to identify it
        /// </summary>
        public string Name { get; }
    }
}