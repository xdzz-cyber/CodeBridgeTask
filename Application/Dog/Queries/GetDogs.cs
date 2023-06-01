using Application.Common.Dtos;
using MediatR;

namespace Application.Dog.Queries;

public class GetDogs : IRequest<List<DogDto>>
{
    
}