using System.Net;

namespace QuakerZero
{
    public class ListenerContext
    {
        public HttpListenerContext HttpListenerContext { get; set; } = null!;
        public CancellationTokenSource CancellationTokenSource { get; set; } = null!;
        public MethodNode MethodNode { get; set; } = null!;
    }
}
