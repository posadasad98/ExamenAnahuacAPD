using System;
using System.Collections.Generic;

namespace DL;

public partial class Genero
{
    public byte IdGenero { get; set; }

    public string? NombreGenero { get; set; }

    public virtual ICollection<Cuento> Cuentos { get; set; } = new List<Cuento>();
}
