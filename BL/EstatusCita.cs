using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstatusCita
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    var query = (from estatusCitaDB in context.EstatusCitas
                                 select new
                                 {
                                     IdEstatusCita = estatusCitaDB.IdEstatusCita,
                                     Nombre = estatusCitaDB.Nombre,
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in query)
                        {
                            ML.EstatusCita estatusCita = new ML.EstatusCita();
                            estatusCita.IdEstatusCita = objBD.IdEstatusCita;
                            estatusCita.Nombre = objBD.Nombre;

                            result.Objects.Add(estatusCita);
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
