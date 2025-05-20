using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
    }
}