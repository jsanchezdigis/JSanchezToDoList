using Microsoft.EntityFrameworkCore;
using ML;
using System.Globalization;

namespace BL;

public class Tarea
{
    public static Result Add(ML.Tarea tarea)
    {
        Result result = new();
        try
        {
            using DL.JsanchezLaudexContext context = new();
            DateTime fecha = DateTime.ParseExact(tarea.FechaVencimiento!,"yyyy-MM-dd",CultureInfo.InvariantCulture);
            var query = context.Database.ExecuteSql($"TareaAdd {tarea.Titulo},{tarea.Descripcion},{fecha.ToString("dd/MM/yyyy")}");
            if (query >= 1)
            {
                result.Correct = true;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un error al insertar la tarea";
            }
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.ErrorMessage = ex.Message;
            result.Ex = ex;
        }
        return result;
    }

    public static Result Update(ML.Tarea tarea)
    {
        Result result = new();
        try
        {
            using DL.JsanchezLaudexContext context = new();
            DateTime fecha = DateTime.ParseExact(tarea.FechaVencimiento!, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var query = context.Database.ExecuteSql($"TareaUpdate {tarea.IdTarea},{tarea.Titulo},{tarea.Descripcion},{fecha:dd/MM/yyyy},{tarea.Estado}");
            if (query >= 1)
            {
                result.Correct = true;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un error al actualizar la tarea";
            }
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.ErrorMessage = ex.Message;
            result.Ex = ex;
        }
        return result;
    }

    public static Result Delete(int IdTarea)
    {
        Result result = new();
        try
        {
            using DL.JsanchezLaudexContext context = new();
            var query = context.Database.ExecuteSql($"TareaDelete {IdTarea}");
            if (query >= 1)
            {
                result.Correct = true;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un error al inactivar la tarea";
            }
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.ErrorMessage = ex.Message;
            result.Ex = ex;
        }
        return result;
    }

    public static Result GetAll(short? Estado)
    {
        Result result = new();
        try
        {
            using DL.JsanchezLaudexContext context = new();
            var query = context.Tareas.FromSql($"TareaGetAll {Estado}").ToList();
            if (query != null)
            {
                result.Objects = [];
                foreach (var item in query)
                {
                    ML.Tarea tarea = new()
                    {
                        IdTarea = item.Idtarea,
                        Titulo = item.Titulo,
                        Descripcion = item.Descripcion,
                        FechaVencimiento = item.Fechavencimiento.ToString(),
                        Estado = item.Estado
                    };
                    result.Objects.Add(tarea);
                }
            }
            result.Correct = true;
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.ErrorMessage = ex.Message;
            result.Ex = ex;
        }
        return result;
    }

    public static Result GetById(int IdTarea)
    {
        Result result = new();
        try
        {
            using DL.JsanchezLaudexContext context = new();
            var query = context.Tareas.FromSql($"TareaGetById {IdTarea}").AsEnumerable().FirstOrDefault();
            if (query != null)
            {
                result.Object = new();
                ML.Tarea tarea = new()
                {
                    IdTarea = query.Idtarea,
                    Titulo = query.Titulo,
                    Descripcion = query.Descripcion,
                    FechaVencimiento = query.Fechavencimiento.ToString(),
                    Estado = query.Estado
                };
                result.Object = tarea;
                result.Correct = true;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "No se logro encontrar la tarea";
            }
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.ErrorMessage = ex.Message;
            result.Ex = ex;
        }
        return result;
    }
}
