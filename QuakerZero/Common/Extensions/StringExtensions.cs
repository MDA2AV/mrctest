namespace QuakerZero
{
    /// <summary>
    /// Extensions for string
    /// </summary>
    public static class StringExtensions{
        /// <summary>
        /// Split Url Parameters from the RawUrl
        /// </summary>
        /// <param name="requestRawUrl"></param>
        /// <returns>string[]</returns>
        public static string[] SplitUrlParameters(this string[] requestRawUrl){
            string[] urlParametersCollection; //Split parameters
            //Try to split parameters, if IndexOutOfRangeException is generated, set parameters to empty string[]
            try{ urlParametersCollection = requestRawUrl[1].Split('&'); }
            catch (System.IndexOutOfRangeException) { urlParametersCollection = new string[] { }; }
            return urlParametersCollection;
        }
    }
}
