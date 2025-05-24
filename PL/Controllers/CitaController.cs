using ML;
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

        [HttpGet]
        public ActionResult Form(int? IdCita)
        {
            ML.Cita cita = new ML.Cita();

            if (IdCita == null)
            {
                cita.Piso = new ML.Piso();
                cita.EstatusCita = new ML.EstatusCita();
                cita.Candidato = new ML.Candidato();
            }
            else
            {
                ML.Result result = BL.Cita.GetById(IdCita.Value);
                cita = (ML.Cita)result.Object;
            }

            ML.Result resultCandidato = BL.Candidato.GetById(2);

            cita.Candidato = (ML.Candidato)resultCandidato.Object;

            ML.Result resultDDLPiso = BL.Piso.GetAll();
            cita.Piso.Pisos = resultDDLPiso.Objects;

            ML.Result resultDDLEstatusCita = BL.EstatusCita.GetAll();
            cita.EstatusCita.EstatusCitas = resultDDLEstatusCita.Objects;


            return View(cita);
        }

        [HttpPost]
        public ActionResult Form(ML.Cita cita)
        {

            //if (cita.IdCita == 0)
            //{
            //    ML.Result result = BL.Cita.Add(cita);

            //}
            //else
            //{
            //    ML.Result result = BL.Cita.Update(cita);
            //}

            return RedirectToAction("GetAll");
        }

    }
}