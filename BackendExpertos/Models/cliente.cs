namespace BackendExpertos.Models;

public partial class cliente
{
    public int id { get; set; }

    public string nombre { get; set; } = null!;

    public string email { get; set; } = null!;

    public long telefono { get; set; }

    public string? direccion { get; set; }

    public string preferencias { get; set; } = null!;

    public bool notificaciones { get; set; }

    public virtual ICollection<resena> resenas { get; set; } = new List<resena>();

    public virtual ICollection<solicitude> solicitudes { get; set; } = new List<solicitude>();
}
