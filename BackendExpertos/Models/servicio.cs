using System;
using System.Collections.Generic;

namespace BackendExpertos.Models;

public partial class servicio
{
    public int id { get; set; }

    public string nombre { get; set; } = null!;

    public string categoria { get; set; } = null!;

    public string descripcion { get; set; } = null!;

    public decimal precioBase { get; set; }

    public int duracionEstimada { get; set; }

    public int? profesional_id { get; set; }

    public bool activo { get; set; }

    public string? estado { get; set; }

    public virtual categoria categoriaNavigation { get; set; } = null!;

    public virtual profesionale? profesional { get; set; }

    public virtual ICollection<resena> resenas { get; set; } = new List<resena>();

    public virtual ICollection<solicitude> solicitudes { get; set; } = new List<solicitude>();
}
