using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        public bool Correct { get; set; } //TRUE = Proceso hecho correctamente // FALSE = Hubo un error
        public string ErrorMessage { get; set; } //Cual es el error especifico
        public Exception Ex { get; set; } //traer TODO el error a detalle
        public object Object { get; set; } //MOSTRAR 1 registro
        public List<object> Objects { get; set; } //MOSTRAR N Registros
    }
}
