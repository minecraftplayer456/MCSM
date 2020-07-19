namespace MCSM
{
    public interface IApplication
    {
        void Start();

        void Stop();
    }

    public class Application : IApplication
    {
        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}