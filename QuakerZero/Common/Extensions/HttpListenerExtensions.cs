using Newtonsoft.Json.Linq;
using System.Net;

namespace QuakerZero
{
    /// <summary>
    /// Custom extensions for HttpListener
    /// </summary>
    public static class HttpListenerExtensions
    {
        /// <summary>
        /// Flush response to client
        /// </summary>
        /// <param name="response"></param>
        /// <param name="buffer"></param>
        public static void FlushBuffer(this HttpListenerResponse response, byte[] buffer)
        {
            //JSON content type by deault
            response.ContentType = "application/json";
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }
        /// <summary>
        /// Flush response to client, Async version.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static async Task FlushBufferAsync(this HttpListenerResponse response, byte[] buffer) { await response.FlushBufferAsync(buffer, "application/json"); }
        public static async Task FlushBufferAsync(this HttpListenerResponse response, byte[] buffer, string contentType)
        {
            //JSON content type by deault
            response.ContentType = contentType;
            response.ContentLength64 = buffer.Length;
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        } //overloaded to allow contentType access
        /// <summary>
        /// Parse request body to JObject
        /// </summary>
        /// <param name="request"></param>
        /// <returns>JObject</returns>
        public static JObject GetBody(this HttpListenerRequest request)
        {
            JObject bodyObject; //Pre-init
            //Try to parse request body to JObject bodyObject
            //If it fails due to any reason (major reason is request having no body)
            //Initialize bodyObject as "{\"message\":\"No Body\"}"
            try
            {
                using (Stream body = request.InputStream)
                {
                    using (StreamReader reader = new StreamReader(body, request.ContentEncoding))
                    {
                        string requestBody = reader.ReadToEnd();
                        bodyObject = JObject.Parse(requestBody);
                    }
                }
            }
            catch (Exception) { bodyObject = JObject.Parse("{\"message\":\"No Body\"}"); }
            return bodyObject;
        }
    }
}
