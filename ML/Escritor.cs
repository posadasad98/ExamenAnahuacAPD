using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Escritor
    {
        public int? IdEscritor {  get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Foto { get; set; }
        public List<object>? Escritors { get; set; }
        public ML.Cuento Cuentos { get; set; }
    }
}
