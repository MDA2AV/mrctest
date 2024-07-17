using Microsoft.AspNetCore.Mvc;
using SimpleW;

namespace MRCTest.Controllers{
    internal class ControllerBaseW : Controller{
        protected string extractProblemDetails(ValidationProblemDetails details){
            string ret = string.Empty;
            foreach (var error in details.Errors){
                string[] messages = error.Value;
                foreach (var message in messages){ ret += $"{error.Key} -> {message}\n"; }
            }
            return ret;
        }
        protected object handleResult(object o) => o;
        protected string handleProblemDetails(ValidationProblemDetails details) => extractProblemDetails(details);
    }
}
