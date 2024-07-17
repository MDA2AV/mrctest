namespace QuakerZero
{
    public interface IEvoker
    {
        Task<IActionResult> Evoke(ListenerContext context);
        void PopulateEndpointsMetadata();
        MethodNode GetMethodNode(string controllerRoute, string endpointRoute, string targetHttpMethodType);
    }
}
