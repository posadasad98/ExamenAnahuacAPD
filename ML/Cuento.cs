using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cuento
    {
        public int? IdCuento { get; set; }
        public string NombreCuento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string TextoCuento { get; set; }
        public string Portada { get; set; }
        public byte[] Referencias { get; set; }
        public List<object>? Cuentos { get; set; }
        public ML.Genero? Genero { get; set; }
        public ML.Escritor? Escritor { get; set; }
    }
}
