using ML;
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
            ML.Empresa empresa = new ML.Empresa();

            ML.Result result = BL.Empresa.GetAll();

            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                empresa.Empresas = new List<object>();
            }
            return View(empresa);
        }

        [HttpGet]
        public ActionResult Form(int? idEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();

            if (idEmpresa != null)
            {
                ML.Result result = BL.Empresa.GetById(idEmpresa.Value);

                empresa = (ML.Empresa)result.Object;
            }

            return View(empresa);
        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {

            if (empresa.IdEmpresa == 0)
            {
                ML.Result result = BL.Empresa.Add(empresa);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se agrego la empresa de manera correcta";
                    return PartialView("_Notificacion");
                }
                else
                {
                    ViewBag.MensajeError = "Hubo un error al agregar la empresa";
                    return PartialView("_Notificacion");
                }
            }
            else
            {
                ML.Result result = BL.Empresa.Update(empresa);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se actualizo la empresa de manera correcta";
                    return PartialView("_Notificacion");
                }
                else
                {
                    ViewBag.MensajeError = "Hubo un error al actualizar la empresa";
                    return PartialView("_Notificacion");
                }
            }

        }

        [HttpGet]
        public ActionResult Delete(int IdEmpresa)
        {
            ML.Result result = BL.Empresa.Delete(IdEmpresa);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Empresa eliminada";
                return PartialView("_Notificacion");
            }
            else
            {
                ViewBag.MensajeError = "No se pudo eliminar la empresa";
                return PartialView("_Notificacion");
            }
        }

    }
}