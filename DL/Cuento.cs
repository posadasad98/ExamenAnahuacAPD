using System;
using System.Collections.Generic;

namespace DL;

public partial class Cuento
{
    public int IdCuento { get; set; }

    public int? IdEscritor { get; set; }

    public string? NombreCuento { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? TextoCuento { get; set; }

    public string? Portada { get; set; }

    public byte[]? Referencias { get; set; }

    public byte? IdGenero { get; set; }

    public virtual Escritor? IdEscritorNavigation { get; set; }

    public virtual Genero? IdGeneroNavigation { get; set; }
}
