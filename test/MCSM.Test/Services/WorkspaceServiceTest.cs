using System.IO;
using MCSM.Models;
using MCSM.Services;
using MCSM.Test.Util;
using MCSM.Util;
using Xunit;

namespace MCSM.Test.Services
{
    [Collection("IO")]
    public class WorkspaceServiceTest : IClassFixture<InitializeClassFixture>
    {
        private const string WorkspacePath = Constants.DefaultWorkspacePath;
        private const string WorkspaceName = Constants.DefaultWorkspaceName;

        [Fact]
        public void When_NoWorkspaceFound_Then_CreateNew()
        {
            if (WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath)) Directory.Delete(WorkspacePath, true);

            Assert.NotNull(WorkspaceService.Default.CreateWorkspace(WorkspacePath, WorkspaceName));

            Assert.True(WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath));
        }

        [Fact]
        public void When_NoWorkspaceFound_Then_NotDeleteIt()
        {
            if (WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath)) Directory.Delete(WorkspacePath, true);

            Assert.False(WorkspaceService.Default.DeleteWorkspace(new Workspace(WorkspaceName)));
        }

        [Fact]
        public void When_NoWorkspaceJsonFound_Then_NotValid()
        {
            if (!WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath))
                WorkspaceService.Default.CreateWorkspace(WorkspacePath, WorkspaceName);

            File.Delete(WorkspacePath + "/workspace.json");

            Assert.False(WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath));
        }

        [Fact]
        public void When_WorkspaceFound_Then_DeleteIt()
        {
            var workspace = WorkspaceService.Default.CreateWorkspace(WorkspacePath, WorkspaceName);

            Assert.True(WorkspaceService.Default.DeleteWorkspace(workspace));

            Assert.False(WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath));
        }

        [Fact]
        public void When_WorkspaceFound_Then_NoCreate()
        {
            if (!WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath))
                WorkspaceService.Default.CreateWorkspace(WorkspacePath, WorkspaceName);

            Assert.Null(WorkspaceService.Default.CreateWorkspace(WorkspacePath, WorkspaceName));
        }
    }
}