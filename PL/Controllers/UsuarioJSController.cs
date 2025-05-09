using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioJSController : Controller
    {
        [HttpGet]
        public ActionResult GetAllJS()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;

            ML.Result result = new ML.Result();

            result = BL.Usuario.GetAllEF(usuario);

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult AddUsuario(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.AddEF(usuario);

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public JsonResult DDLRoles(ML.Rol rol)
        {
            ML.Result result = new ML.Result();

            result = BL.Rol.GetAllLINQ(); ;

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public JsonResult DDLEstados(ML.Estado estado)
        {
            ML.Result result = new ML.Result();

            result = BL.Estado.GetAllEF(); ;

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult UpdateUsuario(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.UpdateEF(usuario); ;

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public JsonResult UsuarioGetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.GetByIdEF(IdUsuario); ;

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult UsuarioDelete(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.DeleteEF(IdUsuario); ;

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}