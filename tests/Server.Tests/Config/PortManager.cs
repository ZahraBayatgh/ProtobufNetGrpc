using System.Threading;

namespace Server.Tests.Config
{
    public class PortManager
    {
        private static int _sPort;
        static PortManager()
        {
            _sPort = 14000;
        }
        public static int GetNextPort() => Interlocked.Increment(ref _sPort);
    }

}
