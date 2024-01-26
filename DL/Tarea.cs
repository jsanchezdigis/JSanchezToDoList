using System;
using System.Collections.Generic;

namespace DL;

public partial class Tarea
{
    public int Idtarea { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateOnly Fechavencimiento { get; set; }

    public short Estado { get; set; }
}
