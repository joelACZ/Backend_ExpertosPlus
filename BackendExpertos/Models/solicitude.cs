namespace BackendExpertos.Models;

public partial class solicitude
{
    public int id { get; set; }

    public int cliente_id { get; set; }

    public int servicio_id { get; set; }

    public DateTime fecha { get; set; }

    public string estado { get; set; } = null!;

    public string nivelUrgencia { get; set; } = null!;

    public string descripcion { get; set; } = null!;

    public string ubicacion { get; set; } = null!;

    public DateTime? fechaCreacion { get; set; }

    public DateTime? fechaActualizacion { get; set; }

    public virtual cliente cliente { get; set; } = null!;

    public virtual servicio servicio { get; set; } = null!;
}
