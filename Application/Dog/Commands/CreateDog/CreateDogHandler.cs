using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dog.Commands.CreateDog;

public class CreateDogHandler : IRequestHandler<CreateDog, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateDogHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(CreateDog request, CancellationToken cancellationToken)
    {
        // Check if dog with same name already exists
        var dogExists = await _context.Dogs.AnyAsync(d => d.Name == request.Name, cancellationToken);
        
        if (dogExists)
        {
            throw new AlreadyExists("Dog with same name already exists", request.Name);
        }
        
        var dog = new Domain.Dog
        {
            Name = request.Name,
            Color = request.Color,
            TailLength = request.TailLength,
            Weight = request.Weight
        };
        
        await _context.Dogs.AddAsync(dog, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);

        return dog.Id;
    }
}