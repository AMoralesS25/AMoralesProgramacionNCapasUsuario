using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;

            //Llenar el DDL de Roles de busqueda abierta
            ML.Result resultDDLRol = BL.Rol.GetAllLINQ();
            usuario.Rol.Roles = resultDDLRol.Objects;

            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {

            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;
            usuario.Rol.IdRol = usuario.Rol.IdRol == 0 ? 0 : usuario.Rol.IdRol;


            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }
            usuario.Rol = new ML.Rol(); //SE ABRIO LA PUERTA

            ML.Result resultDDL = BL.Rol.GetAllLINQ(); //todos lo roles 
            usuario.Rol.Roles = resultDDL.Objects;
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {

            ML.Usuario usuario = new ML.Usuario(); // ES VACIA
            //LA PUERTA CON ROL ESTA CERRRADA 
            usuario.Rol = new ML.Rol();


            if (IdUsuario == null)
            {
                //ADD //VACIO
                usuario.Rol = new ML.Rol(); //SE ABRIO LA PUERTA
                usuario.Direccion = new ML.Direccion(); //SE ABRIO LA PUERTA A DIRECCION
                usuario.Direccion.Colonia = new ML.Colonia(); //SE ABRIO LA PUERTA A COLONIA
                usuario.Direccion.Colonia.Municipio = new ML.Municipio(); //SE ABRIO LA PUERTA A MUNICIPIO
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado(); //SE ABRIO LA PUERTA A ESTADO
            }
            else
            {
                //actualizar //lleno //GetById
                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                ////result.object /unboxing 
                usuario = (ML.Usuario)result.Object; //usuario lleno

                ML.Result resultDDLMunicipio = BL.Municipio.GetByIdEstadoEF(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultDDLMunicipio.Objects;

                ML.Result resultDDLColonia = BL.Colonia.GetByIdMunicipioEF(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultDDLColonia.Objects;
            }

            ML.Result resultDDL = BL.Rol.GetAllLINQ(); //todos lo roles 
            usuario.Rol.Roles = resultDDL.Objects; //le paso todos los valores a roles para que se pueda ver en el DDL
            //usuario.Rol = new ML.Rol(); //SE ABRIO LA PUERTA PERO SI LA VUELVO A ABRIR MIS DATOS SE SOBRESCRIBEN Y TODO SERA NULO

            ML.Result estadosDDL = BL.Estado.GetAllEF();//Todos los estados
            usuario.Direccion.Colonia.Municipio.Estado.Estados = estadosDDL.Objects;//paso los valores de estado

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            HttpPostedFileBase file = Request.Files["inptFileImagen"];

            if (file != null && file.ContentLength > 0)
            {
                usuario.Imagen = ConvertirAArrayBytes(file);
            }
            if (ModelState.IsValid)
            {
                if (usuario.IdUsuario == 0)
                {
                    ML.Result result = BL.Usuario.AddEF(usuario);

                    if (result.Correct)
                    {
                        ViewBag.MensajeVerificado = "Se agrego el usuario de manera correcta";
                        return PartialView("_NotificacionDeEstado");
                    }
                    else
                    {
                        ViewBag.MensajeVerificado = "Hubo un error al agregar el usuario";
                        return PartialView("_NotificacionDeEstado");
                    }
                }
                else
                {
                    ML.Result result = BL.Usuario.UpdateEF(usuario);
                    if (result.Correct)
                    {
                        ViewBag.MensajeVerificado = "Se actualizo el usuario de manera correcta";
                        return PartialView("_NotificacionDeEstado");
                    }
                    else
                    {
                        ViewBag.MensajeVerificado = "Hubo un error al actualizar el usuario";
                        return PartialView("_NotificacionDeEstado");
                    }
                }
            }
            else
            {
                ML.Result resultDDLMunicipio = BL.Municipio.GetByIdEstadoEF(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultDDLMunicipio.Objects;

                ML.Result resultDDLColonia = BL.Colonia.GetByIdMunicipioEF(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultDDLColonia.Objects;

                ML.Result resultDDL = BL.Rol.GetAllLINQ();
                usuario.Rol.Roles = resultDDL.Objects;

                ML.Result estadosDDL = BL.Estado.GetAllEF();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = estadosDDL.Objects;

                return View(usuario);
            }
        }

        public byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {

            ML.Result result = BL.Usuario.DeleteEF(IdUsuario);
            if (result.Correct)
            {
                ViewBag.MensajeVerificado = "Se elimino el usuario de manera correcta";
                return PartialView("_NotificacionDeEstado");
            }
            else
            {
                ViewBag.MensajeVerificado = "Hubo un error al eliminar el usuario";
                return PartialView("_NotificacionDeEstado");
            }
        }

        [HttpPost]
        public JsonResult CambioEstatus(int IdUsuario, bool Estatus)
        {
            ML.Result JsonResult = BL.Usuario.CambiarEstatus(IdUsuario, Estatus);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetByIdEstado(int idEstado)
        {
            ML.Result JsonResult = BL.Municipio.GetByIdEstadoEF(idEstado);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetByIdMunicipio(int idMunicipio)
        {
            ML.Result JsonResult = BL.Colonia.GetByIdMunicipioEF(idMunicipio);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CargaMasiva()
        {
            if (Session["RutaExcel"] == null)
            {
                HttpPostedFileBase excelUsuario = Request.Files["inptFileExcel"];
                string extensionPermitida = ".xlsx";

                if (excelUsuario.ContentLength > 0)
                {
                    string extensionObtenida = Path.GetExtension(excelUsuario.FileName);

                    if (extensionObtenida == extensionPermitida)
                    {
                        string ruta = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(excelUsuario.FileName) + "-" +
                            DateTime.Now.ToString("ddMMyyyyHmmssff") + ".xlsx";
                        if (!System.IO.File.Exists(ruta))
                        {
                            excelUsuario.SaveAs(ruta);
                            string cadenaConexion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + ruta;
                            ML.Result resultExcel = BL.Usuario.LeerExcel(cadenaConexion);

                            if (resultExcel.Correct)
                            {
                                ML.ResultExcel resultValidacion = BL.Usuario.ValidarExcel(resultExcel.Objects);

                                if (resultValidacion.Errores.Count > 0)
                                {
                                    //hubo errores en el excel mostrar tabla 
                                    ViewBag.ErroresExcel = resultValidacion.Errores;
                                    return PartialView("_ErroresExcel");
                                }
                                else
                                {
                                    //ya lei y valide el excel
                                    Session["RutaExcel"] = ruta;
                                    ViewBag.MensajeVerificado = "No tienes errores en tu excel, oprime el boton insertar";
                                    return PartialView("_ErroresExcel");

                                }
                            }
                            else
                            {
                                ViewBag.Errores = "¡El archivo Excel que ingresaste esta vacío!";
                                return PartialView("_ErroresExcel");

                            }
                        }
                        else
                        {
                            //vista parcial, vuleve a cargar el archivo porque ya existe 
                            ViewBag.Errores = "¡El archivo Excel que ingresaste ya existe!";
                            return PartialView("_ErroresExcel");
                        }
                    }
                    else
                    {
                        //vista parcial no es excel el archivo
                        ViewBag.Errores = "¡El archivo que ingresaste no es un Excel!";
                        return PartialView("_ErroresExcel");
                    }
                }
                else
                {
                    //vista parcial no me diste archivok
                    ViewBag.Errores = "¡No ingresaste ningun archivo!";
                    return PartialView("_ErroresExcel");
                }
            }
            else
            {
                //insertar
                string cadenaConexionRuta = ConfigurationManager.ConnectionStrings["OleDbConnection"] + Session["RutaExcel"].ToString();
                ML.Result resultLeerExcel = BL.Usuario.LeerExcel(cadenaConexionRuta);

                if (resultLeerExcel.Objects.Count > 0)
                {
                    ML.ResultExcel resultInsertExcel = new ML.ResultExcel();
                    resultInsertExcel.Errores = new List<object>();

                    int contadorInsert = 1;
                    bool existErrores = false;

                    //leyo bien 
                    foreach (ML.Usuario usuario in resultLeerExcel.Objects)
                    {
                        ML.ResultExcel resultInsert = new ML.ResultExcel();

                        resultInsert.NumeroRegistro = contadorInsert;

                        ML.Result resultInsertar = BL.Usuario.AddEF(usuario);
                        if (!resultInsertar.Correct)
                        {
                            //mostrar error message 
                            resultInsert.ErrorMessage += "Hubo un error al insertar";
                            existErrores = true;
                        }
                        else
                        {
                            resultInsert.ErrorMessage += "Se inserto de manera correcta";
                        }
                        if (resultInsert.ErrorMessage != null)
                        {
                            resultInsertExcel.Errores.Add(resultInsert);
                        }
                        contadorInsert++;
                    }

                    if (existErrores)
                    {
                        //hubo errores al insertar mostrar tabla 
                        ViewBag.ErroresExcel = resultInsertExcel.Errores;
                        Session["RutaExcel"] = null;
                        return PartialView("_ErroresExcel");
                    }
                    else
                    {
                        ViewBag.MensajeVerificado = "Todos los inserts fueron realizados con exito";
                        Session["RutaExcel"] = null;
                        return PartialView("_NotificacionDeEstado");
                    }


                }

            }//archivo

            Session["RutaExcel"] = null;
            return RedirectToAction("GetAll", "Usuario");
        }
    }
}