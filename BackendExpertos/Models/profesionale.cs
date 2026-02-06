using System;
using System.Collections.Generic;

namespace BackendExpertos.Models;

public partial class profesionale
{
    public int id { get; set; }

    public string nombre { get; set; } = null!;

    public string email { get; set; } = null!;

    public long telefono { get; set; }

    public string? direccion { get; set; }

    public string especialidad { get; set; } = null!;

    public string oficios { get; set; } = null!;

    public int experiencia { get; set; }

    public bool disponibilidad { get; set; }

    public virtual ICollection<servicio> servicios { get; set; } = new List<servicio>();
}
