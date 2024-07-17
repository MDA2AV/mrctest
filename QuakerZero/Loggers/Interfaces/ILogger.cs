namespace QuakerZero
{
    public interface ILogger{
        void WriteLine<TId, TMessage>(TId id, TMessage message);
    }
}
