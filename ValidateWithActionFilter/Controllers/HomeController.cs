using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValidateWithActionFilter.ActionFilters;

namespace ValidateWithActionFilter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateReCaptcha]
        public ActionResult TestRecaptcha()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}