using NUnit.Framework;
using AutoFixture;
using ML;

namespace Test;

[TestFixture]
public class TareaTest
{
    private readonly Random _random;
    private readonly Fixture _fixture;
    public TareaTest() 
    {
        _random = new Random();
        _fixture = new Fixture();
    }

    [Test]
    public void AddTareaCorrectTest()
    {
        // Arrange
        Tarea tarea = new()
        {
            Titulo = _fixture.Create<string>()[..30],
            Descripcion = _fixture.Create<string>()[..30],
            FechaVencimiento = _fixture.Create<DateTime>().ToString("yyyy-MM-dd")
        };

        // Act
        Result result = BL.Tarea.Add(tarea);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Correct, Is.True);
            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.Ex, Is.Null);
        });
    }

    [Test]
    public void AddTareaErrorTest()
    {
        // Arrange
        Tarea tarea = new()
        {
            Titulo = null,
            Descripcion = _fixture.Create<string>()[..30],
            FechaVencimiento = _fixture.Create<DateTime>().ToString("yyyy-MM-dd")
        };

        // Act
        Result result = BL.Tarea.Add(tarea);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Correct, Is.False);
            Assert.That(result.ErrorMessage, Is.Not.Null);
            Assert.That(result.Ex, Is.Not.Null);
        });
    }

    [Test]
    public void UpdateTareaCorrectTest()
    {
        // Arrange
        Tarea tareaAdd = new()
        {
            Titulo = _fixture.Create<string>()[..30],
            Descripcion = _fixture.Create<string>()[..30],
            FechaVencimiento = _fixture.Create<DateTime>().ToString("yyyy-MM-dd")
        };

        BL.Tarea.Add(tareaAdd);
        using DL.JsanchezLaudexContext context = new();
        var list = ConversionHelper.ConvertFromDL([.. context.Tareas]);

        Tarea buscar = list.FirstOrDefault(t => t.Titulo == tareaAdd.Titulo
                                                && t.Descripcion == tareaAdd.Descripcion
                                                && t.FechaVencimiento == tareaAdd.FechaVencimiento)!;

        Tarea tareaUpdate = new()
        {
            IdTarea = buscar.IdTarea,
            Titulo = _fixture.Create<string>()[..30],
            Descripcion = _fixture.Create<string>()[..30],
            FechaVencimiento = _fixture.Create<DateTime>().ToString("yyyy-MM-dd"),
            Estado = (short)_random.Next(0, 2)
        };

        // Act
        Result result = BL.Tarea.Update(tareaUpdate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Correct, Is.True);
            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.Ex, Is.Null);
        });
    }

    [Test]
    public void UpdateTareaIncorrectTest()
    {
        // Arrange
        Tarea tarea = new()
        {
            IdTarea = 0,
            Titulo = _fixture.Create<string>()[..30],
            Descripcion = _fixture.Create<string>()[..30],
            FechaVencimiento = _fixture.Create<DateTime>().ToString("yyyy-MM-dd"),
            Estado = (short)_random.Next(0, 2)
        };

        // Act
        Result result = BL.Tarea.Update(tarea);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Correct, Is.False);
            Assert.That(result.ErrorMessage, Is.Not.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo("Ocurrió un error al actualizar la tarea"));
            Assert.That(result.Ex, Is.Null);
        });
    }

    [Test]
    public void DeleteCorrectTest()
    {
        // Arrange
        Tarea tareaAdd = new()
        {
            Titulo = _fixture.Create<string>()[..30],
            Descripcion = _fixture.Create<string>()[..30],
            FechaVencimiento = _fixture.Create<DateTime>().ToString("yyyy-MM-dd")
        };

        BL.Tarea.Add(tareaAdd);
        using DL.JsanchezLaudexContext context = new();
        var list = ConversionHelper.ConvertFromDL([.. context.Tareas]);

        Tarea buscar = list.FirstOrDefault(t => t.Titulo == tareaAdd.Titulo
                                                && t.Descripcion == tareaAdd.Descripcion
                                                && t.FechaVencimiento == tareaAdd.FechaVencimiento)!;

        // Act
        Result result = BL.Tarea.Delete(buscar.IdTarea);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Correct, Is.True);
            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.Ex, Is.Null);
        });
    }

    [Test]
    public void DeleteIncorrectTest()
    {
        // Arrange
        int idTarea = 0;

        // Act
        Result result = BL.Tarea.Delete(idTarea);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Correct, Is.False);
            Assert.That(result.ErrorMessage, Is.Not.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo("Ocurrió un error al inactivar la tarea"));
            Assert.That(result.Ex, Is.Null);
        });
    }

    [Test]
    public void GetAllCorrectTest()
    {
        // Arrange
        short estado = 1;

        // Act
        Result result = BL.Tarea.GetAll(estado);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Correct, Is.True);
            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.Ex, Is.Null);
            Assert.That(result.Objects, Is.Not.Null);
        });
        Assert.That(result.Objects, Is.Not.Empty);
    }

    [Test]
    public void GetByIdXIdCorrectTest()
    {
        // Arrange
        Tarea tareaAdd = new()
        {
            Titulo = _fixture.Create<string>()[..30],
            Descripcion = _fixture.Create<string>()[..30],
            FechaVencimiento = _fixture.Create<DateTime>().ToString("yyyy-MM-dd")
        };

        BL.Tarea.Add(tareaAdd);
        using DL.JsanchezLaudexContext context = new();
        var list = ConversionHelper.ConvertFromDL([.. context.Tareas]);

        Tarea buscar = list.FirstOrDefault(t => t.Titulo == tareaAdd.Titulo
                                                && t.Descripcion == tareaAdd.Descripcion
                                                && t.FechaVencimiento == tareaAdd.FechaVencimiento)!;

        // Act
        Result result = BL.Tarea.GetById(buscar.IdTarea);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Correct, Is.True);
            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.Ex, Is.Null);
            Assert.That(result.Object, Is.Not.Null);
        });
    }

    [Test]
    public void GetByIdXIdIncorrectTest()
    {
        // Arrange
        int idTarea = 0;

        // Act
        Result result = BL.Tarea.GetById(idTarea);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Correct, Is.False);
            Assert.That(result.ErrorMessage, Is.Not.Null);
            Assert.That(result.Ex, Is.Null);
        });
    }

    public static class ConversionHelper
    {
        public static List<Tarea> ConvertFromDL(List<DL.Tarea> tareaDL)
        {
            List<Tarea> tareaList = [];
            foreach (var item in tareaDL)
            {
                Tarea tarea = new()
                {
                    IdTarea = item.Idtarea,
                    Titulo = item.Titulo,
                    Descripcion = item.Descripcion,
                    FechaVencimiento = item.Fechavencimiento.ToDateTime(new TimeOnly(0, 0, 0)).ToString("yyyy-MM-dd"),
                    Estado = item.Estado
                };
                tareaList.Add(tarea);
            }

            return tareaList;
        }
    }
}
