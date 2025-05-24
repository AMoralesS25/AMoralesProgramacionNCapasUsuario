using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class EstatusCita
    {
        [DisplayName("Estatus")]
        public byte IdEstatusCita { get; set; }
        public string Nombre { get; set; }
        public List<object> EstatusCitas { get; set; }
    }
}
