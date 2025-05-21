using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    DL.Empresa empresaBD = new DL.Empresa();
                    empresaBD.Nombre = empresa.Nombre;
                    empresaBD.Longitud = empresa.Longitud;
                    empresaBD.Latitud = empresa.Latitud;

                    context.Empresas.Add(empresaBD);

                    int filasAfectadas = context.SaveChanges();
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar";
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

        public static ML.Result GetById(int idEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                    new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {

                    var query = (from empresaDB in context.Empresas
                                 where empresaDB.IdEmpresa == idEmpresa
                                 select new
                                 {
                                     IdEmpresa = empresaDB.IdEmpresa,
                                     Nombre = empresaDB.Nombre,
                                     Longitud = empresaDB.Longitud,
                                     Latitud = empresaDB.Latitud
                                 }).SingleOrDefault();


                    if (query != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();
                        empresa.IdEmpresa = query.IdEmpresa;
                        empresa.Nombre = query.Nombre;
                        empresa.Latitud = query.Latitud;
                        empresa.Longitud = query.Longitud;

                        result.Object = empresa; 
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registro";
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

        public static ML.Result Update(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                    new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {

                    var query = (from empresaSelect in context.Empresas
                                 where empresaSelect.IdEmpresa == empresa.IdEmpresa
                                 select empresaSelect).SingleOrDefault();

                    if (query != null)
                    {
                        query.IdEmpresa = empresa.IdEmpresa;
                        query.Nombre = empresa.Nombre;
                        query.Longitud = empresa.Longitud;
                        query.Latitud = empresa.Latitud;

                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al actualizar";
                        }
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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                    new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {

                    var query = (from empresaDB in context.Empresas
                                 select new
                                 {
                                     IdEmpresa = empresaDB.IdEmpresa,
                                     Nombre = empresaDB.Nombre,
                                     Latitud = empresaDB.Latitud,
                                     Longitud = empresaDB.Longitud

                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = objBD.IdEmpresa;
                            empresa.Nombre = objBD.Nombre;
                            empresa.Latitud = objBD.Latitud;
                            empresa.Longitud = objBD.Longitud;

                            result.Objects.Add(empresa);
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

        public static ML.Result Delete(int idEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                    new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    var query = (from empresa in context.Empresas
                                 where empresa.IdEmpresa == idEmpresa
                                 select empresa).SingleOrDefault();

                    if (query != null)
                    {
                        context.Empresas.Remove(query); 
                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo eliminar";
                        }
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
