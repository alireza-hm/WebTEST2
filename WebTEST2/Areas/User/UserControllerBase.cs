using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebTEST2.Areas.User
{
    [Area("User")]
    [Authorize(Policy = "UserPolicy")]
    public class UserControllerBase : Controller
    {

        protected void SuccessAlert(string message)
        {
            var model = "";
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }

        protected void ErrorAlert(string message)
        {
            var model = "";
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
    }
}