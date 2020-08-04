using System;
using MCSM.Api;

namespace MCSM.Ui.Test.Util
{
    public class TestApplicationLifecycle : IApplicationLifecycle
    {
        public delegate void StartDel(string[] args);
        public delegate void StopDel();

        private readonly StartDel _startDel;
        private readonly StopDel _stopDel;

        public TestApplicationLifecycle(StartDel startDel = null, StopDel stopDel = null)
        {
            _startDel = startDel;
            _stopDel = stopDel;
        }
        
        public void Start(string[] args)
        {
            _startDel?.Invoke(args);
        }

        public void Stop()
        {
            _stopDel?.Invoke();
        }
    }
}