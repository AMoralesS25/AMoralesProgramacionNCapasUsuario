using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Carrera
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                    new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {

                    var query = (from carreraDB in context.Carreras
                                 select new
                                 {
                                     IdCarrera = carreraDB.IdCarrera,
                                     Nombre = carreraDB.Nombre,
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in query)
                        {
                            ML.Carrera carrera = new ML.Carrera();
                            carrera.IdCarrera = objBD.IdCarrera;
                            carrera.Nombre = objBD.Nombre;

                            result.Objects.Add(carrera);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de carreras";
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
