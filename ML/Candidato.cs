using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Candidato
    {
        public int IdCandidato { get; set; }
        [Required(ErrorMessage = "Escribe el username")]
        [RegularExpression(@"^[A-Za-z0-9]{5,50}$", ErrorMessage = "El nombre de usuario debe tener al menos un numero y letras")]
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
        public string ImagenBase64 { get; set; }
        public byte[] Curriculum { get; set; }
        public List<object> Candidatos { get; set; }
        public ML.Universidad Universidad { get; set; }
        public ML.Carrera Carrera { get; set; }
        public ML.BolsaTrabajo BolsaTrabajo { get; set; }
        public ML.Vacante Vacante { get; set; }
    }
}
