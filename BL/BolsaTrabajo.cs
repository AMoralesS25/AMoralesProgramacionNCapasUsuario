using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BolsaTrabajo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                    new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {

                    var query = (from bolsaTrabajoDB in context.BolsaTrabajoes
                                 select new
                                 {
                                     IdBolsaTrabajo = bolsaTrabajoDB.IdBolsaTrabajo,
                                     Nombre = bolsaTrabajoDB.Nombre,
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in query)
                        {
                            ML.BolsaTrabajo bolsaTrabajo = new ML.BolsaTrabajo();
                            bolsaTrabajo.IdBolsaTrabajo = objBD.IdBolsaTrabajo;
                            bolsaTrabajo.Nombre = objBD.Nombre;

                            result.Objects.Add(bolsaTrabajo);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de bolsas de trabajo";
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
