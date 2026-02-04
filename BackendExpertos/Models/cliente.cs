using System;
using System.Collections.Generic;

namespace BackendExpertos.Models;

public partial class cliente
{
    public int id { get; set; }

    public string? nombre { get; set; }

    public string? email { get; set; }

    public string? telefono { get; set; }

    public string? direccion { get; set; }

    public string? preferencias { get; set; }

    public bool? notificaciones { get; set; }

    public string? preferenciasFormateadas { get; set; }

    public string? notificacionesFormateada { get; set; }

    public virtual ICollection<resena> resenas { get; set; } = new List<resena>();

    public virtual ICollection<solicitude> solicitudes { get; set; } = new List<solicitude>();
}
