using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipioEF(int idMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new
                    DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    var query = context.ColoniaGetByIdMunicipio(idMunicipio).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objDB in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = objDB.IdColonia;
                            colonia.Nombre = objDB.Nombre;

                            result.Objects.Add(colonia);
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
