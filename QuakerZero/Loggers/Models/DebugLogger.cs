using System.Diagnostics;

namespace QuakerZero{
    public class DebugLogger : ILogger{
        public void WriteLine<TId, TMessage>(TId id, TMessage message) { Debug.WriteLine($"<{id}> : {message}"); }
    }
}
