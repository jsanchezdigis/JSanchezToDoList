namespace ML;

public class Tarea
{
    public int IdTarea { get; set; }

    public string? Titulo { get; set; }

    public string? Descripcion { get; set; }

    public string? FechaVencimiento { get; set; }

    public short? Estado { get; set; }

    public List<object> Tareas { get; set; }
}
