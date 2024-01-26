using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers;

public class TareaController : Controller
{
    [HttpGet]
    public IActionResult GetAll()
    {
        short? estado = null;
        Tarea tarea = new();
        Result result = BL.Tarea.GetAll(estado);
        if (result.Correct)
        {
            tarea.Tareas = result.Objects;
            return View(tarea);
        }
        else
        {
            return View(tarea);
        }
    }

    [HttpPost]
    public IActionResult GetAll(short? Estado)
    {
        Tarea tarea = new();
        Result result = BL.Tarea.GetAll(Estado);
        if (result.Correct)
        {
            tarea.Tareas = result.Objects;
            return Json(result);
            //return View(tarea);
        }
        else
        {
            return Json(result);
            //return View(tarea);
        }
    }

    [HttpGet]
    public IActionResult Form(int IdTarea)
    {
        if (IdTarea == 0)
        {
            Tarea tarea = new() 
            {
                FechaVencimiento = DateTime.Now.ToString("dd/MM/yyyy")
            };
            return View(tarea);
        }

        Result result = BL.Tarea.GetById(IdTarea);
        if (result.Correct)
        {
            Tarea tarea = (Tarea)result.Object;
            return View(tarea);
        }
        else
        {
            ViewBag.Message = result.ErrorMessage;
            return PartialView("Modal");
        }
    }

    [HttpPost]
    public IActionResult Form(Tarea tarea)
    {
        if (tarea.IdTarea == 0)
        {
            Result result = BL.Tarea.Add(tarea);
            if (result.Correct)
            {
                ViewBag.Message = "Se registro correctamente la tarea";
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al registrar la tarea";
            }
        }
        else
        {
            Result result = BL.Tarea.Update(tarea);
            if (result.Correct)
            {
                ViewBag.Message = "Se actualizo correctamente la tarea";
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al actualizar la tarea";
            }
        }
        return PartialView("Modal");
    }

    [HttpGet]
    public IActionResult Delete(int IdTarea)
    {
        Result result = BL.Tarea.Delete(IdTarea);
        if (result.Correct)
        {
            ViewBag.Message = "Se inactivo correctamente la tarea";
        }
        else
        {
            ViewBag.Message = "Ocurrió un error al inactivar la tarea";
        }
        return PartialView("Modal");
    }
}
