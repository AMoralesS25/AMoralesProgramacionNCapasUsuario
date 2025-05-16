using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace PL.Controllers
{
    public class CandidatoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            

            ML.Candidato candidato = new ML.Candidato();
            candidato.Candidatos = new List<object>();
            ML.Result resultDDLVacante = BL.Vacante.GetAll();
            candidato.Vacante=new ML.Vacante();
            candidato.Vacante.Vacantes = resultDDLVacante.Objects;

            return View(candidato);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Candidato candidato)
        {
            int idVacante = candidato.Vacante.IdVacante;

            idVacante = idVacante == 0 ? 0 : idVacante;

            ML.Result result = BL.Candidato.GetAll(idVacante);

            if (result.Correct)
            {
                candidato.Candidatos = result.Objects;
            }
            else
            {
                candidato.Candidatos = new List<object>();
            }

            candidato.Vacante = new ML.Vacante();

            ML.Result resultDDLVacante = BL.Vacante.GetAll();
            candidato.Vacante.Vacantes = resultDDLVacante.Objects;
            candidato.Vacante.IdVacante = idVacante;

            return View(candidato);
        }


        [HttpGet]
        public ActionResult Form(int? IdCandidato)
        {
            ML.Candidato candidato = new ML.Candidato();

            if (IdCandidato == null)
            {
                candidato.Universidad = new ML.Universidad();
                candidato.Carrera = new ML.Carrera();
                candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                candidato.Vacante = new ML.Vacante();
            }
            else
            {
                ML.Result result = BL.Candidato.GetById(IdCandidato.Value);
                candidato = (ML.Candidato)result.Object;
            }

            ML.Result resultDDLUniversidad = BL.Universidad.GetAll();
            candidato.Universidad.Universidades = resultDDLUniversidad.Objects;

            ML.Result resultDDLCarrera = BL.Carrera.GetAll();
            candidato.Carrera.Carreras = resultDDLCarrera.Objects;

            ML.Result resultDDLBolsaTrabajo = BL.BolsaTrabajo.GetAll();
            candidato.BolsaTrabajo.BolsasTrabajos = resultDDLBolsaTrabajo.Objects;

            ML.Result resultDDLVacante = BL.Vacante.GetAll();
            candidato.Vacante.Vacantes = resultDDLVacante.Objects;

            return View(candidato);
        }

        [HttpPost]
        public ActionResult Form(ML.Candidato candidato)
        {
            HttpPostedFileBase fileFoto = Request.Files["inptFileFoto"];

            if (fileFoto != null && fileFoto.ContentLength > 0)
            {
                candidato.Foto = ConvertirAArrayBytes(fileFoto);
            }

            HttpPostedFileBase fileCurriculum = Request.Files["inptFileCurriculum"];

            if (fileCurriculum != null && fileCurriculum.ContentLength > 0)
            {
                candidato.Curriculum = ConvertirAArrayBytes(fileCurriculum);
            }

            if (candidato.IdCandidato == 0)
            {
                ML.Result result = BL.Candidato.Add(candidato);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Candidato agregado";
                    return PartialView("_Notificacion");
                }
                else
                {
                    ViewBag.MensajeError = "No se pudo agregar el candidato";
                    return PartialView("_Notificacion");
                }
            }
            else
            {
                ML.Result result = BL.Candidato.Update(candidato);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Candidato actualizado";
                    return PartialView("_Notificacion");
                }
                else
                {
                    ViewBag.MensajeError = "No se pudo actualizar el candidato";
                    return PartialView("_Notificacion");
                }
            }

        }

        [HttpGet]
        public ActionResult Delete(int IdCandidato)
        {

            ML.Result result = BL.Candidato.Delete(IdCandidato);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Candidato eliminado";
                return PartialView("_Notificacion");
            }
            else
            {
                ViewBag.MensajeError = "No se pudo eliminar el candidato";
                return PartialView("_Notificacion");
            }
        }

        [NonAction]
        public byte[] ConvertirAArrayBytes(HttpPostedFileBase Archivo)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Archivo.InputStream);
            byte[] data = reader.ReadBytes((int)Archivo.ContentLength);
            return data;
        }

        [HttpGet]
        public FileResult DescargarCurriculum(int? IdCandidato)
        {
            ML.Result result = BL.Candidato.GetById(IdCandidato.Value);
            ML.Candidato candidato = (ML.Candidato)result.Object;

            byte[] archivoCurriculum = candidato.Curriculum;

            string nombreArchivo = "Curriculum_" + candidato.Nombre + candidato.ApellidoPaterno + candidato.ApellidoMaterno + ".pdf";

            if (archivoCurriculum != null && archivoCurriculum.Length > 0)
            {
                return File(archivoCurriculum, System.Net.Mime.MediaTypeNames.Application.Octet, nombreArchivo);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult SendEmail()
        {
            try
            {
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"].ToString());
                bool useDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["useDefaultCredentials"].ToString());
                string correo = ConfigurationManager.AppSettings["correo"].ToString();
                string password = ConfigurationManager.AppSettings["password"].ToString();
                bool enableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"].ToString());

                string body = "";
                string pathHTML = Server.MapPath("~/Content/Correo/PlantillaCorreo.html");
                string pathImg = Server.MapPath("~/Content/Correo/IMG-WELCOME.jpg");

                StreamReader reader = new StreamReader(pathHTML);

                body = reader.ReadToEnd();

                body=body.Replace("{{NombreUsuario}}", "PRUEBA");

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = port,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = new NetworkCredential(correo, password),
                    EnableSsl = enableSsl
                };

                var mensaje = new MailMessage
                {
                    From = new MailAddress(correo, "Adriana Morales"),
                    Subject = "Correo de prueba",
                    Body = body,
                    IsBodyHtml = true
                };

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                LinkedResource imagen = new LinkedResource(pathImg)
                {
                    ContentId = "imagen", 
                    ContentType = new System.Net.Mime.ContentType("image/jpeg")
                };

                htmlView.LinkedResources.Add(imagen);

                mensaje.AlternateViews.Add(htmlView);

                mensaje.To.Add("adrianamsan25@gmail.com");
                smtpClient.Send(mensaje);

                ViewBag.Mensaje = "Correo enviado";
                return PartialView("_Notificacion");
            }
            catch (Exception ex)
            {
                ViewBag.MensajeError = "Hubo un error al enviar el correo";
                return PartialView("_Notificacion");
            }
        }
    }
}
