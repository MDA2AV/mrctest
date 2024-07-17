namespace QuakerZero
{
    public interface IApp
    {
        Task Run();
        IList<Type> MiddlewareTypes { get; set; }
    }
}
