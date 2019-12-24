using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.MVC.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            //throw new Exception("异常");
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
