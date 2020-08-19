using MCSM.Api;

namespace MCSM.Ui.Test.Util
{
    /// <summary>
    ///     Class to hock up in ApplicationLifecycle
    /// </summary>
    public class TestApplicationLifecycle : IApplicationLifecycle
    {
        /// <summary>
        ///     Delegate that will be called instead of start method
        /// </summary>
        /// <param name="args">arguments from cli</param>
        public delegate void StartDel(string[] args);

        /// <summary>
        ///     Delegate that will be called instead of stop method
        /// </summary>
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
            //Execute start delegate in task
            _startDel?.Invoke(args);
        }

        public void Stop()
        {
            //Execute stop delegate
            _stopDel?.Invoke();
        }
    }
}