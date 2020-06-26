using System;
using System.IO;
using MCSM.Util;
using MCSM.Util.IO;

namespace MCSM.Test.Util
{
    public class InitializeClassFixture : IDisposable
    {
        public InitializeClassFixture()
        {
            LogUtil.Initialize();

            if (Directory.Exists(Constants.DefaultWorkspacePath))
                Directory.Delete(Constants.DefaultWorkspacePath, true);
            Paths.Workspace.Initialize(Constants.DefaultWorkspacePath);
        }

        public void Dispose()
        {
        }
    }
}