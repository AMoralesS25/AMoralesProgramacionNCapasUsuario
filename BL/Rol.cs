using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {

                    //Primero se busca con un select
                    var query = (from rolDB in context.Rols
                                 select new
                                 {
                                     IdRol = rolDB.IdRol,
                                     Nombre = rolDB.Nombre,
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in query)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = objBD.IdRol;
                            rol.Nombre = objBD.Nombre;

                            result.Objects.Add(rol);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
