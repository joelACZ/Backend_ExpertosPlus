using System;
using System.Collections.Generic;

namespace BackendExpertos.Models;

public partial class solicitude
{
    public int id { get; set; }

    public int? cliente_id { get; set; }

    public int? profesional_id { get; set; }

    public int? servicio_id { get; set; }

    public string? estado { get; set; }

    public string? descripcion { get; set; }

    public string? ubicacion { get; set; }

    public bool? urgencia { get; set; }

    public DateTime? fecha { get; set; }

    public virtual cliente? cliente { get; set; }

    public virtual profesionale? profesional { get; set; }

    public virtual servicio? servicio { get; set; }
}
