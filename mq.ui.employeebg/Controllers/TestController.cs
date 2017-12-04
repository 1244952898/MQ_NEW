using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mq.ui.employeebg.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult A()
        {
            return View();
        }

        [HttpGet]
		public JsonResult Test(string data) {
			if (string.IsNullOrEmpty(data))
			{
                return Json(new { Code = "E001", Msg = "空数据" }, JsonRequestBehavior.AllowGet); 
			}
			else
			{
                return Json(new { Code = "E000", Msg = data }, JsonRequestBehavior.AllowGet); 
			}
		}
	}
}