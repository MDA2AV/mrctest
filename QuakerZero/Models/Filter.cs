namespace QuakerZero
{
    public interface IActionFilter
    {
        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="parameters"></param>
        void OnActionExecuting(ListenerContext context, object[] parameters);
        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="parameters"></param>
        void OnActionExecuted(ListenerContext context, object[] parameters);
    }
    /// <summary>
    /// Filter object, contains IActionFilter Action filter, an instance that implements logic to execute before and after the endpoint.
    /// Parameters are the input object[] to IActionFilter methods.
    /// </summary>
    public class Filter
    {
        public IActionFilter ActionFilter { get; set; } = null!;
        public object[] Parameters { get; set; } = null!;
    }
}
