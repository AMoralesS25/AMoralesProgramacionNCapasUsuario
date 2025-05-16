using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class CitaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DDLVacante()
        {
            ML.Result result = new ML.Result();

            result = BL.Vacante.GetAll();

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public JsonResult GetAllCita(int IdVacante)
        {
            ML.Result result = new ML.Result();

            result = BL.Cita.GetAll(IdVacante);

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}