using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Candidato
    {
        public static ML.Result GetAll(int idVacante)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                    new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    var query = context.CandidatoGetAll(idVacante).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in query)
                        {
                            ML.Candidato candidato = new ML.Candidato();
                            candidato.Universidad = new ML.Universidad();
                            candidato.Carrera = new ML.Carrera();
                            candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                            candidato.Vacante = new ML.Vacante();
                            candidato.IdCandidato = objBD.IdCandidato;
                            candidato.Nombre = objBD.NombreCandidato;
                            candidato.ApellidoPaterno = objBD.ApellidoPaterno;
                            candidato.ApellidoMaterno = objBD.ApellidoMaterno;
                            candidato.Edad = objBD.Edad;
                            candidato.Correo = objBD.Correo;
                            candidato.Telefono = objBD.Telefono;
                            candidato.Direccion = objBD.Direccion;
                            candidato.Foto = objBD.Foto;
                            candidato.Curriculum = objBD.Curriculum;
                            candidato.Universidad.Nombre = objBD.NombreUniversidad;
                            candidato.Carrera.Nombre = objBD.NombreCarrera;
                            candidato.BolsaTrabajo.Nombre = objBD.NombreBolsaTrabajo;
                            candidato.Vacante.Nombre = objBD.NombreVacante;

                            result.Objects.Add(candidato);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de candidatos";
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

        public static ML.Result Add(ML.Candidato candidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new
                    DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    int rowsAffect = context.CandidatoAdd(candidato.Nombre, candidato.ApellidoPaterno,
                        candidato.ApellidoMaterno, candidato.Edad, candidato.Correo, candidato.Telefono, candidato.Direccion, candidato.Foto, candidato.Curriculum, candidato.Universidad.IdUniversidad, candidato.Carrera.IdCarrera, candidato.BolsaTrabajo.IdBolsaTrabajo, candidato.Vacante.IdVacante);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo insertar el registro del candidato";
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


        public static ML.Result GetById(int idCandidato)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                    new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    var query = context.CandidatoGetById(idCandidato).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Candidato candidato = new ML.Candidato();
                        candidato.Universidad = new ML.Universidad();
                        candidato.Carrera = new ML.Carrera();
                        candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                        candidato.Vacante = new ML.Vacante();
                        candidato.IdCandidato = query.IdCandidato;
                        candidato.Nombre = query.NombreCandidato;
                        candidato.ApellidoPaterno = query.ApellidoPaterno;
                        candidato.ApellidoMaterno = query.ApellidoMaterno;
                        candidato.Edad = query.Edad;
                        candidato.Correo = query.Correo;
                        candidato.Telefono = query.Telefono;
                        candidato.Direccion = query.Direccion;
                        candidato.Foto = query.Foto;
                        candidato.Curriculum = query.Curriculum;
                        candidato.Universidad.IdUniversidad = query.IdUniversidad.HasValue ? query.IdUniversidad.Value : 0;
                        candidato.Carrera.IdCarrera = query.IdCarrera.HasValue ? query.IdCarrera.Value : 0;
                        candidato.BolsaTrabajo.IdBolsaTrabajo = query.IdBolsaTrabajo.HasValue ? query.IdBolsaTrabajo.Value : 0;
                        candidato.Vacante.IdVacante = query.IdVacante.HasValue ? query.IdVacante.Value : 0;

                        result.Object = candidato;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existe el registro del candidato";
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

        public static ML.Result Update(ML.Candidato candidato)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new
                    DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    int rowsAffect = context.CandidatoUpdate(candidato.IdCandidato, candidato.Nombre, candidato.ApellidoPaterno,
                        candidato.ApellidoMaterno, candidato.Edad, candidato.Correo, candidato.Telefono, candidato.Direccion, candidato.Foto, candidato.Curriculum, candidato.Universidad.IdUniversidad, candidato.Carrera.IdCarrera, candidato.BolsaTrabajo.IdBolsaTrabajo, candidato.Vacante.IdVacante);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el registro del candidato";
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

        public static ML.Result Delete(int idCandidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new
                    DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    int rowsAffect = context.CandidatoDelete(idCandidato);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el registro del candidato";
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
