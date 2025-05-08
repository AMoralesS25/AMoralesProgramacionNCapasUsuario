using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class BolsaTrabajo
    {
        [DisplayName("Bolsa de Trabajo")]
        public int IdBolsaTrabajo { get; set; }
        public string Nombre { get; set; }
        public List<object> BolsasTrabajos { get; set; }
    }
}
