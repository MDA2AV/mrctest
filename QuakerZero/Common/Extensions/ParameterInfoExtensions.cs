using System.Reflection;

namespace QuakerZero
{
    /// <summary>
    /// Extensions for ParameterInfo
    /// </summary>
    public static class ParameterInfoExtensions{
        /// <summary>
        /// Creates an object[] which contains the endpoint input variables by its signature order
        /// </summary>
        /// <param name="parameterInfos"></param>
        /// <param name="parameters"></param>
        /// <returns>object[]</returns>
        public static List<object> FilterParameters(this ParameterInfo[] parameterInfos, List<Parameter> parameters){
            List<object> _params = new List<object>(); //Initialize dynamic collection that will contain the input variables
            //Cycle through the ParameterInfos for the endpoint signature
            foreach (ParameterInfo pInfo in parameterInfos){
                //Cycle through the parameters obtained from the request Url
                //and attempt to match one of them with the current ParameterInfo
                foreach (Parameter parameter in parameters){
                    //Check if parameters name match
                    if (parameter.Name == pInfo.Name){
                        //Matching parameter found!
                        //Convert the Url string parameter to its intended type
                        //
                        //int?
                        if (pInfo.ParameterType.ToString() == "System.Int32"){ _params.Add(Convert.ToInt32(parameter.Value)); }
                        //string?
                        else if (pInfo.ParameterType.ToString() == "System.String"){ _params.Add(parameter.Value); }
                        //double?
                        else if (pInfo.ParameterType.ToString() == "System.Double"){ _params.Add(Convert.ToDouble(parameter.Value)); }
                        //bool?
                        else if (pInfo.ParameterType.ToString() == "System.Boolean"){ _params.Add(Convert.ToBoolean(parameter.Value)); }
                    }
                }
            }
            return _params;
        }
    }
}
