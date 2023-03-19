using lab1.Filters;
using lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;

namespace lab1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    private static List<Car> _cars = new();
    private readonly ILogger<CarsController> _logger;

    public CarsController(ILogger<CarsController> logger) 
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Car>> GetAll()
    {
        _logger.LogInformation($"Incoming request: {Request.Method} {Request.Path}");
        return _cars;
    }

    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<Car> GetById(int id)
    {
        var car = _cars.FirstOrDefault(m => m.Id == id);
        if (car is null)
        {
            return NotFound(new GeneralResponse("Resource is missing"));
        }
        return car;
    }

    [HttpPost]
    [Route("v1")]
    public ActionResult Add(Car car)
    {
        car.Type = "Gas";
        _cars.Add(car);
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = car.Id },
            value: new GeneralResponse("Resource is added"));
    }

    [HttpPost]
    [Route("v2")]
    [ServiceFilter(typeof(ValidateCarTypeAttribute))]
    public ActionResult AddV2(Car car)
    {
        _cars.Add(car);
        return CreatedAtAction(
            actionName: nameof(GetAll),
            value: new GeneralResponse("Thanks for adding"));
    }


    [HttpPut]
    [Route("{id:min(1)}")]
    public ActionResult Edit(Car car, int id)
    {
        if (id != car.Id)
        {
            return BadRequest(new GeneralResponse("Ids don't match"));
        }

        var carToEdit = _cars.FirstOrDefault(m => m.Id == id);

        if (carToEdit is null)
        {
            return NotFound(new GeneralResponse("Resource is missing"));
        }

        carToEdit.ProductionDate = car.ProductionDate;
        carToEdit.Model = car.Model;
        carToEdit.Price = car.Price;

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete(int id)
    {
        var carToDelete = _cars.FirstOrDefault(m => m.Id == id);

        if (carToDelete is null)
        {
            return NotFound(new GeneralResponse("Resource is missing"));
        }

        _cars.Remove(carToDelete);

        return NoContent();
    }
}
