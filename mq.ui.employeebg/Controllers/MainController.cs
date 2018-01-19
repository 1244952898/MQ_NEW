using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mq.ui.employeebg.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult MainPage()
        {
            return View("~/Views/Share/_MainPage.cshtml");
        }
    }
}