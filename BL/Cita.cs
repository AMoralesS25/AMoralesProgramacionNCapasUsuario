using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cita
    {
        public static ML.Result GetAll(int idVacante)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                    new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    var query = (from candidatoDB in context.Candidatoes
                                 join universidadDB in context.Universidads on candidatoDB.IdUniversidad equals universidadDB.IdUniversidad into univGroup
                                 from universidadDB in univGroup.DefaultIfEmpty()
                                 join carreraDB in context.Carreras on candidatoDB.IdCarrera equals carreraDB.IdCarrera into carreraGroup
                                 from carreraDB in carreraGroup.DefaultIfEmpty()
                                 join bolsaDB in context.BolsaTrabajoes on candidatoDB.IdBolsaTrabajo equals bolsaDB.IdBolsaTrabajo into bolsaGroup
                                 from bolsaDB in bolsaGroup.DefaultIfEmpty()
                                 join vacanteDB in context.Vacantes on candidatoDB.IdVacante equals vacanteDB.IdVacante into vacanteGroup
                                 from vacanteDB in vacanteGroup.DefaultIfEmpty()
                                 join citaDB in context.Citas on candidatoDB.IdCandidato equals citaDB.IdCandidato into citaGroup
                                 from citaDB in citaGroup.DefaultIfEmpty()
                                 where candidatoDB.IdVacante == idVacante
                                 select new
                                 {
                                     candidatoDB.IdCandidato,
                                     NombreCandidato = candidatoDB.Nombre,
                                     candidatoDB.ApellidoPaterno,
                                     candidatoDB.ApellidoMaterno,
                                     candidatoDB.Edad,
                                     candidatoDB.Correo,
                                     candidatoDB.Telefono,
                                     candidatoDB.Direccion,
                                     candidatoDB.Foto,
                                     candidatoDB.Curriculum,
                                     NombreUniversidad = universidadDB != null ? universidadDB.Nombre : "No disponible",
                                     NombreCarrera = carreraDB != null ? carreraDB.Nombre : "No disponible",
                                     NombreBolsaTrabajo = bolsaDB != null ? bolsaDB.Nombre : "No disponible",
                                     NombreVacante = vacanteDB != null ? vacanteDB.Nombre : "No disponible",
                                     IdCita = citaDB != null ? citaDB.IdCita : (int?)null
                                 }).ToList();


                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in query)
                        {
                            ML.Cita cita = new ML.Cita();
                            cita.Candidato = new ML.Candidato();
                            cita.Candidato.Vacante=new ML.Vacante();
                            cita.IdCita = objBD.IdCita.HasValue ? objBD.IdCita.Value : 0;
                            cita.Candidato.Nombre = objBD.NombreCandidato;
                            cita.Candidato.ApellidoPaterno=objBD.ApellidoPaterno;
                            cita.Candidato.ApellidoMaterno=objBD.ApellidoMaterno;
                            cita.Candidato.Edad=objBD.Edad;
                            cita.Candidato.Correo=objBD.Correo;
                            cita.Candidato.Telefono=objBD.Telefono;
                            cita.Candidato.Vacante.Nombre=objBD.NombreVacante;
                            cita.Candidato.Foto=objBD.Foto;
                            cita.Candidato.ImagenBase64 = (objBD.Foto != null) ? Convert.ToBase64String(objBD.Foto) : null;

                            result.Objects.Add(cita);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de citas";
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
