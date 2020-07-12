using System;
using MCSM.Util.IO;

namespace MCSM.Test.Util
{
    public class InitializeClassFixture : IDisposable
    {
        public InitializeClassFixture()
        {
            LogUtil.Initialize();

            /*if (Directory.Exists(Constants.DefaultWorkspacePath))
                Directory.Delete(Constants.DefaultWorkspacePath, true);
            WorkspaceService.Default.CreateWorkspace(Constants.DefaultWorkspaceName, Constants.DefaultWorkspacePath);*/
        }

        public void Dispose()
        {
        }
    }
}