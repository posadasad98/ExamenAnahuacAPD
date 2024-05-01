﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class Rol
{
    public int IdRol { get; set; }

    public string? TipoRol { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
