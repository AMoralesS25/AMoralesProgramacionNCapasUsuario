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
            ML.Cita cita = new ML.Cita();
            cita.Citas = new List<object>();
            ML.Result resultDDLVacante = BL.Vacante.GetAll();
            cita.Candidato = new ML.Candidato();
            cita.Candidato.Vacante = new ML.Vacante();
            cita.Candidato.Vacante.Vacantes = resultDDLVacante.Objects;

            return View(cita);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Cita cita)
        {
            int idVacante = cita.Candidato.Vacante.IdVacante;

            idVacante = idVacante == 0 ? 0 : idVacante;

            ML.Result result = BL.Cita.GetAll(idVacante);

            if (result.Correct)
            {
                cita.Citas = result.Objects;
            }
            else
            {
                cita.Citas = new List<object>();
            }

            cita.Candidato.Vacante = new ML.Vacante();

            ML.Result resultDDLVacante = BL.Vacante.GetAll();
            cita.Candidato.Vacante.Vacantes = resultDDLVacante.Objects;
            cita.Candidato.Vacante.IdVacante = idVacante;

            return View(cita);
        }

        [HttpGet]
        public ActionResult Form(int? IdCita, int IdCandidato)
        {
            ML.Cita cita = new ML.Cita();

            if (IdCita == 0)
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

            ML.Result resultCandidato = BL.Candidato.GetById(IdCandidato);

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

            if (cita.IdCita == 0)
            {
                ML.Result result = BL.Cita.Add(cita);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se agrego la cita de manera correcta";
                    return PartialView("_Notificacion");
                }
                else
                {
                    ViewBag.MensajeError = "Hubo un error al agregar la cita";
                    return PartialView("_Notificacion");
                }
            }
            else
            {
                ML.Result result = BL.Cita.Update(cita);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se agrego la cita de manera correcta";
                    return PartialView("_Notificacion");
                }
                else
                {
                    ViewBag.MensajeError = "Hubo un error al agregar la cita";
                    return PartialView("_Notificacion");
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdCita)
        {

            ML.Result result = BL.Cita.Delete(IdCita);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Cita eliminada";
                return PartialView("_Notificacion");
            }
            else
            {
                ViewBag.MensajeError = "No se pudo eliminar la cita";
                return PartialView("_Notificacion");
            }
        }

    }
}