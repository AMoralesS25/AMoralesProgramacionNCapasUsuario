using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstadoEF(int idEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    var query = context.MunicipioGetByIdEstado(idEstado).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objDB in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = objDB.IdMunicipio;
                            municipio.Nombre = objDB.Nombre;

                            result.Objects.Add(municipio);
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
