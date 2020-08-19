namespace MCSM
{
    /// <summary>
    ///     Main entry point for application
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //Creates new application instance, run it and wait for completion
            var application = new Application();
            application.Start(args).Wait();
        }
    }
}