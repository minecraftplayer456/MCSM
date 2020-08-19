using System.Threading.Tasks;
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

        public Task Start(string[] args)
        {
            return new Task(() => _startDel?.Invoke(args));
        }

        public void Stop()
        {
            _stopDel?.Invoke();
        }
    }
}