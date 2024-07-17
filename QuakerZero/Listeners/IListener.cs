namespace QuakerZero
{
    public interface IListener
    {
        Task Listen(IList<Type> middlewareTypes, IBuilderOptions options);
    }
}
