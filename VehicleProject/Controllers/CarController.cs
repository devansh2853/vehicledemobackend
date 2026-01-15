using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleProject.Data;
using VehicleProject.Models;
using VehicleProject.Services.Interfaces;

namespace VehicleProject.Controllers;
[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    public CarController(ICarService carService)
    {
        _carService =  carService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Car>>> GetCars()
    {
        try {
            return Ok(await _carService.GetAllAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet("{id:int}", Name = "GetCar")]
    public async Task<ActionResult<Car>> Get(int id)
    {
        try
        {    
            var car = await _carService.GetByIdAsync(id);
            if (car == null) return NotFound("Car with the given ID was not found");
            return Ok(car);
        }
        catch (Exception err) {
            return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateCar(Car car)
    {
        try
        {
            var createdCar = await _carService.CreateAsync(car);
            return CreatedAtRoute("GetCar", new {id = createdCar.Id}, createdCar);
        }
        catch (Exception err)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCar(int id, [FromBody ]Car car)
    {
        try
        {
            var updated = await _carService.UpdateAsync(id, car);
            if (updated == UpdateResult.Success) return NoContent();
            else if (updated == UpdateResult.IdMismatch) return BadRequest("Id mismatch");
            else if (updated == UpdateResult.NotFound) return NotFound("Car with the given ID was not found");
            else return  StatusCode(StatusCodes.Status500InternalServerError);
        }
        catch (Exception err)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task <IActionResult> DeleteCar(int id)
    {
        try
        {
            var deleted= await  _carService.DeleteAsync(id);
            if (deleted) return NoContent();
            else return NotFound("Car with the given ID was not found");
        }
        catch (Exception err)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
        }
    }
    [HttpGet("fields")]
    public ActionResult GetCarFields()
    {
        try
        {
            var fields = typeof(Car)
            .GetProperties()
            .Where(p=>p.Name != "Id")
            .Select(p=>new {
                name=p.Name, 
                type=p.PropertyType.Name,
                required=Attribute.IsDefined(p, typeof(RequiredAttribute))
                });
            return Ok(fields);
        }
        catch (Exception err)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
        }
        

    }
}