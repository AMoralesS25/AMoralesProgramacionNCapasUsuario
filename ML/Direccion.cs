using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        [DisplayName("Calle")]
        [Required(ErrorMessage = "Escribe la calle")]
        [RegularExpression(@"^[A-Za-z0-9\s]{1,50}$", ErrorMessage = "La calle solo debe tener letras y números")]
        public string Calle { get; set; }
        [DisplayName("Numero interior")]
        [Required(ErrorMessage = "Escribe la número interior")]
        [RegularExpression(@"^[A-Za-z0-9\s]{1,20}$", ErrorMessage = "El número interior solo debe tener letras y números")]
        public string NumeroInterior { get; set; }
        [DisplayName("Número Exterior")]
        [Required(ErrorMessage = "Escribe el número exterior")]
        [RegularExpression(@"^[A-Za-z0-9\s]{1,50}$", ErrorMessage = "La calle solo debe tener letras y números")]
        public string NumeroExterior { get; set; }
        public ML.Colonia Colonia { get; set; }
        public List<object> Direcciones { get; set; }
    }
}
