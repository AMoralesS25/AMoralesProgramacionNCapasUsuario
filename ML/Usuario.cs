using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [DisplayName("Username")]
        [Required(ErrorMessage = "Escribe el username")]
        [RegularExpression(@"^[A-Za-z0-9]{5,50}$", ErrorMessage = "El nombre de usuario debe tener al menos un numero y letras")]
        public string UserName { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Escribe el nombre")]
        [RegularExpression(@"^[A-Za-z ]{1,50}$", ErrorMessage = "El nombre solo debe tener letras")]
        public string Nombre { get; set; }
        [DisplayName("Apellido Paterno")]
        [Required(ErrorMessage = "Escribe el apellido paterno")]
        [RegularExpression(@"^[A-Za-z ]{1,50}$", ErrorMessage = "El apellido paterno solo debe tener letras")]
        public string ApellidoPaterno { get; set; }
        [DisplayName("Apellido Materno")]
        [Required(ErrorMessage = "Escribe el apellido materno")]
        [RegularExpression(@"^[A-Za-z ]{1,50}$", ErrorMessage = "El apellido materno solo debe tener letras")]
        public string ApellidoMaterno { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Escribe el email")]
        [RegularExpression(@"^[\w+._-]{4,254}@[a-zA-Z0-9.-]{2,254}\.[a-zA-Z]{2,10}$", ErrorMessage = "El email no es valido")]
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Escribe el password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@@$!%*?&])([A-Za-z\d$@@$!%*?&]|[^ ]){8,50}$", ErrorMessage = "El password debe contener al menos un número, una mayuscula y un signo")]
        public string Password { get; set; }
        //public DateTime FechaNacimiento { get; set; }
        [DisplayName("Fecha de nacimiento")]
        [Required(ErrorMessage = "Escribe la fecha de nacimiento")]
        [RegularExpression(@"\d{1,2}\/\d{1,2}\/\d{2,4}", ErrorMessage = "La fecha debe de incluir numeros")]
        public string FechaNacimiento { get; set; }
        [DisplayName("Sexo")]
        public string Sexo { get; set; }
        [DisplayName("Telefono")]
        [Required(ErrorMessage = "Escribe el telefono")]
        [RegularExpression(@"^[0-9]{8,20}$", ErrorMessage = "El telefono solo debe incluir números")]
        public string Telefono { get; set; }
        [DisplayName("Celular")]
        [Required(ErrorMessage = "Escribe el celular")]
        [RegularExpression(@"^[0-9]{8,20}$", ErrorMessage = "El celular solo debe incluir números")]
        public string Celular { get; set; }
        public bool Estatus { get; set; }
        [DisplayName("CURP")]
        [Required(ErrorMessage = "Escribe el CURP")]
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage = "El curp no esta correcto")]
        public string CURP { get; set; }
        public byte[] Imagen { get; set; }
        public string ImagenBase64 { get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }
        public List<object> Usuarios { get; set; } //PARA MANDAR INFORMACIÓN A LAS VISTAS 
    }
}
