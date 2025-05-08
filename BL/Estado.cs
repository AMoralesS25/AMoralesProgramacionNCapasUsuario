using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new
                    DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    var query = context.EstadoGetAll().ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objDB in query)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = objDB.IdEstado;
                            estado.Nombre = objDB.Nombre;

                            result.Objects.Add(estado);
                        }
                        result.Correct = true;
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
