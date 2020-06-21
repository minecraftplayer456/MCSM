using System.IO;
using MCSM.Services;
using MCSM.Test.Util;
using MCSM.Util;
using Xunit;

namespace MCSM.Test.Services
{
    public class WorkspaceServiceTest : IClassFixture<InitializeClassFixture>
    {
        private const string WorkspacePath = Constants.DefaultWorkspacePath;
        private const string WorkspaceName = "testWorkspace";

        [Fact]
        public void When_NoWorkspaceFound_Then_CreateNew()
        {
            if (WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath)) Directory.Delete(WorkspacePath, true);

            Assert.True(WorkspaceService.Default.CreateWorkspace(WorkspacePath, WorkspaceName));

            Assert.True(WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath));
        }

        [Fact]
        public void When_NoWorkspaceFound_Then_NotDeleteIt()
        {
            if (WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath)) Directory.Delete(WorkspacePath, true);

            Assert.False(WorkspaceService.Default.DeleteWorkspace(WorkspacePath));
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
        public void When_WorkspaceFound_DeleteIt()
        {
            if (!WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath))
                WorkspaceService.Default.CreateWorkspace(WorkspacePath, WorkspaceName);

            Assert.True(WorkspaceService.Default.DeleteWorkspace(WorkspacePath));

            Assert.False(WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath));
        }

        [Fact]
        public void When_WorkspaceFound_Then_NoCreate()
        {
            if (!WorkspaceService.ValidateWorkspaceDirectory(WorkspacePath))
                WorkspaceService.Default.CreateWorkspace(WorkspacePath, WorkspaceName);

            Assert.False(WorkspaceService.Default.CreateWorkspace(WorkspacePath, WorkspaceName));
        }
    }
}