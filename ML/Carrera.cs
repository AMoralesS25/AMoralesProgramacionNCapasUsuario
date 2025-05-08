using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Carrera
    {
        [DisplayName("Carrera")]
        public int IdCarrera { get; set; }
        public string Nombre { get; set; }
        public List<object> Carreras { get; set; }
    }
}
