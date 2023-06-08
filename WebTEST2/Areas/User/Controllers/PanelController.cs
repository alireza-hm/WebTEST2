using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebTEST2.Areas.User.Controllers
{
    public class PanelController : UserControllerBase
    {
        [Authorize(Roles = "User")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
