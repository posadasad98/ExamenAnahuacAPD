using System;
using System.Collections.Generic;

namespace DL;

public partial class Escritor
{
    public int IdEscritor { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Telefono { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Foto { get; set; }

    public virtual ICollection<Cuento> Cuentos { get; set; } = new List<Cuento>();
}
