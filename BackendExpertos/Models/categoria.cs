using System;
using System.Collections.Generic;

namespace BackendExpertos.Models;

public partial class categoria
{
    public string nombre { get; set; } = null!;

    public virtual ICollection<servicio> servicios { get; set; } = new List<servicio>();
}
