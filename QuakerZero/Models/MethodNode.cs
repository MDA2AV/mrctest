using System.Reflection;

namespace QuakerZero
{
    /// <summary>
    /// Assembly info for an endpoint
    /// 
    /// HasFilters: Does this endpoint have ServiceFilters?
    /// 
    /// Method: MethodInfo for the endpoint call
    /// 
    /// Filters: Collection of IActionFilters with respective parameters
    /// 
    /// ParentController: ControllerBase that owns this endpoint
    /// </summary>
    public class MethodNode
    {
        public bool HasFilters { get; set; } //Does this method have Action Filters?
        public bool AllowAnonymous { get; set; } //Skip Authoriztion?
        public MethodInfo Method { get; set; } = null!; //MethodInfo object for this MethodWithFilter
        public string HttpMethodType { get; set; } = null!; //HttpMethod, one of { HttpPost, HttpGet, HttpPut, HttpDelete, HttpSocket }
        public List<Filter> Filters { get; set; } = null!; //List of IActionFilter, filters to be executed before or after the endpoint
        public ControllerBase ParentContoller { get; set; } = null!; //Parent Controller that owns the endpoint where this Method is implemented
    }
}
