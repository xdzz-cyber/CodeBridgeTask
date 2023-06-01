using Application.Common.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dog.Queries;

public class GetDogsHandler : IRequestHandler<GetDogs, List<DogDto>>
{
    private readonly IApplicationDbContext _context;

    public GetDogsHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<DogDto>> Handle(GetDogs request, CancellationToken cancellationToken)
    {
        var dogs = await _context.Dogs.ToListAsync(cancellationToken);
        
        return dogs.Select(d => new DogDto
        {
            Name = d.Name,
            Color = d.Color,
            TailLength = d.TailLength,
            Weight = d.Weight
        }).ToList();
    }
}