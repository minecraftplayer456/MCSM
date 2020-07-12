using System.IO;
using MCSM.Models;
using MCSM.Util;
using MCSM.Util.IO;
using Serilog;

namespace MCSM.Services
{
    /// <summary>
    ///     Service to control workspace. Workspaces are folders where all data like worlds, mods, servers are stored. It must
    ///     contain a workspace.json file.
    /// </summary>
    public class WorkspaceService : LazyAble<WorkspaceService>
    {
        /// <summary>
        ///     Creates a new empty workspace
        /// </summary>
        /// <param name="workspaceName">Name of the workspace</param>
        /// <param name="workspacePath">Path to workspace directory</param>
        /// <returns>New workspace</returns>
        public Workspace CreateWorkspace(string workspacePath, string workspaceName)
        {
            if (ValidateWorkspaceDirectory(workspacePath))
            {
                Log.Debug("Found workspace in {workspacePath}. Create no new one.", workspacePath);
                //TODO Add loading workspace
                return null;
            }

            Log.Debug("Found no workspace in {workspacePath}. Create new one {workspaceName}", workspacePath,
                workspaceName);

            var workspace = new Workspace(workspaceName);

            workspace.Path.Initialize(workspacePath);

            var workspaceFile = File.CreateText(workspace.JsonPath.AbsolutePath);
            var workspaceJson = JsonUtil.Serialize(workspace);
            workspaceFile.WriteLine(workspaceJson);
            workspaceFile.Close();

            return workspace;
        }

        /// <summary>
        ///     Deletes workspace
        /// </summary>
        /// <param name="workspace">workspace to delete</param>
        /// <returns>True if workspace was deleted successfully</returns>
        public bool DeleteWorkspace(Workspace workspace)
        {
            if (!ValidateWorkspaceDirectory(workspace.Path.AbsolutePath)) return false;

            Directory.Delete(workspace.Path.AbsolutePath, true);

            return true;
        }

        /// <summary>
        ///     Tests that the directory exists
        /// </summary>
        /// <param name="workspacePath">Workspace path to test</param>
        /// <returns></returns>
        public static bool ExistsWorkspaceDirectory(string workspacePath)
        {
            if (Directory.Exists(workspacePath)) return true;
            Log.Debug("{workspacePath} is not a valid directory.", workspacePath);
            return false;
        }

        /// <summary>
        ///     Validates that the workspace path is a folder and contains a workspace.json
        /// </summary>
        /// <param name="workspacePath">Path to directory for validating workspace</param>
        /// <returns></returns>
        public static bool ValidateWorkspaceDirectory(string workspacePath)
        {
            if (!ExistsWorkspaceDirectory(workspacePath)) return false;

            //Look for one workspace.json
            if (Directory.GetFiles(workspacePath, "workspace.json").Length == 1)
            {
                Log.Debug("Workspace json in {workspacePath} found", workspacePath);
                return true;
            }

            Log.Debug("Workspace json was not found in {workspacePath}", workspacePath);
            return false;
        }
    }
}