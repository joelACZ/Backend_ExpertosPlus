using System;
using System.Collections.Generic;

namespace BackendExpertos.Models;

public partial class profesionale
{
    public int id { get; set; }

    public string? nombre { get; set; }

    public string? especialidad { get; set; }

    public string? telefono { get; set; }

    public string? email { get; set; }

    public string? ubicacion { get; set; }

    public string? oficios { get; set; }

    public int? experiencia { get; set; }

    public bool? disponibilidad { get; set; }

    public virtual ICollection<servicio> servicios { get; set; } = new List<servicio>();

    public virtual ICollection<solicitude> solicitudes { get; set; } = new List<solicitude>();
}
