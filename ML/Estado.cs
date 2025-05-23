﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Estado
    {
        [DisplayName("Estado")]
        [Required(ErrorMessage = "Elige un estado")]
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public List<object> Estados { get; set; } //PARA MANDAR INFORMACIÓN A LAS VISTAS 

    }
}
