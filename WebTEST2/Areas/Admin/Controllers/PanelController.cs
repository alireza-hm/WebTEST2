using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebTEST2.Areas.Admin.Controllers
{
    public class PanelController : AdminControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
