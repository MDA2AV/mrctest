namespace QuakerZero
{
    public class ConsoleLogger : ILogger{
        public void WriteLine<TId, TMessage>(TId id, TMessage message){ Console.WriteLine($"<{id}> : {message}"); }
    }
}
