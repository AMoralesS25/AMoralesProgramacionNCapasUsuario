using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Candidato
    {
        public int IdCandidato { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Apellido Paterno")]
        public string ApellidoPaterno { get; set; }
        [DisplayName("Apellido Materno")]
        public string ApellidoMaterno { get; set; }
        public string Edad { get; set; }
        [DisplayName("Correo Electronico")]
        public string Correo { get; set; }
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }
        [DisplayName("Dirección")]
        public string Direccion { get; set; }
        public byte[] Foto { get; set; }
        public byte[] Curriculum { get; set; }
        public List<object> Candidatos { get; set; }
        public ML.Universidad Universidad { get; set; }
        public ML.Carrera Carrera { get; set; }
        public ML.BolsaTrabajo BolsaTrabajo { get; set; }
        public ML.Vacante Vacante { get; set; }
    }
}
