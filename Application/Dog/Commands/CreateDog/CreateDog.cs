using MediatR;

namespace Application.Dog.Commands.CreateDog;

public class CreateDog : IRequest<Guid>
{
    public string Name { get; set; } = null!;
    
    public string Color { get; set; } = null!;
    
    public int TailLength { get; set; }
    
    public int Weight { get; set; }

    public CreateDog(string name, string color, int tailLength, int weight)
    {
        Name = name;
        Color = color;
        TailLength = tailLength;
        Weight = weight;
    }
}