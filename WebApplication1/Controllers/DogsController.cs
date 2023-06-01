using Application.Common.Dtos;
using Application.Dog.Commands.CreateDog;
using Application.Dog.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class DogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DogsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet(Name = "Dogs")]
    public async Task<List<DogDto>> Dogs(string? attribute = "Weight", string? order = "asc", string? pageNumber = "1", string? pageSize = "10")
    {
        var dogs = await _mediator.Send(new GetDogs());

        if (!string.IsNullOrEmpty(attribute))
        {
            dogs = order == "asc" ? dogs.OrderBy(d => d.GetType().GetProperty(attribute)?.GetValue(d, null)).ToList()
                : dogs.OrderByDescending(d => d.GetType().GetProperty(attribute)?.GetValue(d, null)).ToList(); 
        }
        // Implement pagination
         if (!string.IsNullOrEmpty(pageNumber) && !string.IsNullOrEmpty(pageSize))
         {
             dogs = dogs.Skip((int.Parse(pageNumber) - 1) * int.Parse(pageSize)).Take(int.Parse(pageSize)).ToList();
         }
         
        return dogs.ToList();
    }
    
    // Create a new dog handler.
    [HttpPost]
    public async Task<IActionResult> AddDog(DogDto dogDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var createdInstanceId = await _mediator.Send(new CreateDog(dogDto.Name, dogDto.Color, dogDto.TailLength, dogDto.Weight));
        
        // Check if the instance was created successfully.
        if (createdInstanceId == Guid.Empty)
        {
            return BadRequest();
        }
        
        return RedirectToAction("Dogs");
    }
}