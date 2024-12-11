using Microsoft.AspNetCore.Mvc;

namespace MAS_CRMWebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeController : Controller
    {
        [HttpGet("/Admin/Anasayfa")]
        public IActionResult Index()
        {
            var userFullName = HttpContext.Session.GetString("userFullName");
            ViewBag.UserFullName = userFullName;
            return View();
        }
    }
}
