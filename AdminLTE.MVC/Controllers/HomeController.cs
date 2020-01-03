using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.MVC.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Desktop()
        {
            return View();
        }
        public IActionResult Logout()
        {
            //移除Session
            HttpContext.Session.Remove("CurrentUserId");
            HttpContext.Session.Remove("CurrentUser");

            return Json(new { result="Success"});
        }
    }
}
