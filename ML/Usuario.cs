using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario {  get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Rol Rol { get; set; }
    }
}
