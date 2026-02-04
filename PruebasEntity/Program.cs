using BackendExpertos.Contexts;
using BackendExpertos.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new ExpertoContext())
{
    // 1. Crear categoría
    var categoria = new categoria
    {
        nombre = "Electricidad"
    };

    context.categorias.Add(categoria);
    context.SaveChanges(); // Guardamos para que la categoría exista antes de asociarla

    // 2. Crear profesional (Se eliminó 'id =')
    var profesional = new profesionale
    {
        nombre = "Carlos Mendoza",
        especialidad = "Electricista residencial",
        telefono = "0999999999",
        email = "carlos@correo.com",
        ubicacion = "Guayaquil",
        oficios = "Instalaciones, Reparaciones",
        experiencia = 8,
        disponibilidad = true
    };

    context.profesionales.Add(profesional);
    context.SaveChanges(); // Guardamos para obtener el ID generado por la DB

    // 3. Crear servicio
    var servicio = new servicio
    {
        nombre = "Instalación de tomacorrientes",
        categoria = categoria.nombre, // Relación por nombre (según tu Fluent API)
        descripcion = "Instalación y revisión de tomacorrientes",
        precioBase = 25.00m,
        duracionEstimada = 60,
        profesional_id = profesional.id, // ID obtenido automáticamente tras el SaveChanges anterior
        activo = true
    };

    context.servicios.Add(servicio);
    context.SaveChanges();

    // 4. Crear cliente
    var cliente = new cliente
    {
        nombre = "Ana Torres",
        email = "ana@correo.com",
        telefono = "0988888888",
        direccion = "Av. Principal",
        preferencias = "Atención rápida",
        notificaciones = true,
        preferenciasFormateadas = "Atención rápida",
        notificacionesFormateada = "Email"
    };

    context.clientes.Add(cliente);
    context.SaveChanges();

    // 5. Crear solicitud
    var solicitud = new solicitude
    {
        cliente_id = cliente.id,
        profesional_id = profesional.id,
        servicio_id = servicio.id,
        estado = "Pendiente",
        descripcion = "Problema con tomacorriente en sala",
        ubicacion = "Guayaquil",
        urgencia = true,
        fecha = DateTime.Now
    };

    context.solicitudes.Add(solicitud);
    context.SaveChanges();

    // 6. Consulta con Includes
    // Importante: Asegúrate que las propiedades de navegación (cliente, profesional, etc.) 
    // existan en tu clase 'solicitude'.
    var solicitudes = context.solicitudes
        .Include(s => s.cliente)
        .Include(s => s.profesional)
        .Include(s => s.servicio)
            .ThenInclude(se => se.categoriaNavigation)
        .ToList();

    foreach (var s in solicitudes)
    {
        // Usamos el operador null-conditional (?.) por seguridad
        Console.WriteLine(
            $"{s.cliente?.nombre ?? "N/A"} | {s.servicio?.nombre ?? "N/A"} | " +
            $"{s.servicio?.categoriaNavigation?.nombre ?? "Sin Cat"} | " +
            $"{s.profesional?.nombre ?? "N/A"} | {s.estado}"
        );
    }
}