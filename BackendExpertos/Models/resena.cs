using System;
using System.Collections.Generic;

namespace BackendExpertos.Models;

public partial class resena
{
    public int id { get; set; }

    public int? servicio_id { get; set; }

    public int? cliente_id { get; set; }

    public int? calificacion { get; set; }

    public string? comentario { get; set; }

    public DateOnly? fecha { get; set; }

    public bool? anonima { get; set; }

    public virtual cliente? cliente { get; set; }

    public virtual servicio? servicio { get; set; }
}
