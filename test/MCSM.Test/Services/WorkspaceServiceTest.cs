using System.IO.Abstractions.TestingHelpers;
using MCSM.Services;
using MCSM.Services.IO;
using MCSM.Test.Util;
using MCSM.Util;
using Xunit;

namespace MCSM.Test.Services
{
    public class WorkspaceServiceTest : IClassFixture<InitializeClassFixture>
    {
        private const string WorkspacePath = Constants.DefaultWorkspacePath;
        private const string WorkspaceName = Constants.DefaultWorkspaceName;

        [Fact]
        public void When_WorkspaceNotExists_Then_CreateNew()
        {
            var fileSystem = new MockFileSystem();
            var workspaceService = new WorkspaceService(new FileService(fileSystem));

            var workspace = workspaceService.CreateWorkspace(WorkspacePath, WorkspaceName);

            Assert.True(fileSystem.Directory.Exists(workspace.Path.AbsolutePath));
            Assert.True(fileSystem.File.Exists(workspace.JsonPath.AbsolutePath));
        }

        [Fact]
        public void When_WorkspaceExists_Then_GetNull()
        {
            var fileSystem = new MockFileSystem();
            var workspaceService = new WorkspaceService(new FileService(fileSystem));

            var workspace = workspaceService.CreateWorkspace(WorkspacePath, WorkspaceName);

            Assert.Null(workspaceService.CreateWorkspace(WorkspacePath, WorkspaceName));
        }

        [Fact]
        public void When_WorkspaceJsonMission_Then_NoneValidWorkspace()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);
            var workspaceService = new WorkspaceService(fileService);

            var workspace = workspaceService.CreateWorkspace(WorkspacePath, WorkspaceName);

            fileService.Delete(workspace.JsonPath);

            Assert.False(workspaceService.ValidateWorkspaceDirectory(workspace.Path));
        }

        [Fact]
        public void When_WorkspaceExists_Then_Delete()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);
            var workspaceService = new WorkspaceService(fileService);

            var workspace = workspaceService.CreateWorkspace(WorkspacePath, WorkspaceName);

            Assert.True(workspaceService.DeleteWorkspace(workspace));

            Assert.False(fileService.Exists(workspace.Path));
        }

        [Fact]
        public void When_WorkspaceNotExists_Then_GetFalse()
        {
            var fileSystem = new MockFileSystem();
            var fileService = new FileService(fileSystem);
            var workspaceService = new WorkspaceService(fileService);

            var workspace = workspaceService.CreateWorkspace(WorkspacePath, WorkspaceName);

            workspaceService.DeleteWorkspace(workspace);

            Assert.False(workspaceService.DeleteWorkspace(workspace));
        }
    }
}