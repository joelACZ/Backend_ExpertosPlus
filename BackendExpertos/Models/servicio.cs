using System;
using System.Collections.Generic;

namespace BackendExpertos.Models;

public partial class servicio
{
    public int id { get; set; }

    public string? nombre { get; set; }

    public string? categoria { get; set; }

    public string? descripcion { get; set; }

    public decimal? precioBase { get; set; }

    public int? duracionEstimada { get; set; }

    public int? profesional_id { get; set; }

    public bool? activo { get; set; }

    public virtual categoria? categoriaNavigation { get; set; }

    public virtual profesionale? profesional { get; set; }

    public virtual ICollection<resena> resenas { get; set; } = new List<resena>();

    public virtual ICollection<solicitude> solicitudes { get; set; } = new List<solicitude>();
}
