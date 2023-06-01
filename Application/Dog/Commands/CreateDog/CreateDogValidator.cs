using FluentValidation;

namespace Application.Dog.Commands.CreateDog;

public class CreateDogValidator : AbstractValidator<CreateDog>
{
    public CreateDogValidator()
    {
        // Add rules for the properties in the CreateDog command
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
        
        RuleFor(x => x.Color)
            .NotEmpty()
            .MaximumLength(50);
        // Check that TailLength is a number itself, and that it is between 0 and 100
        RuleFor(x => x.TailLength)
            .NotEmpty()
            .LessThan(100)
            .GreaterThan(0);
        
        RuleFor(x => x.Weight)
            .NotEmpty()
            .LessThan(100)
            .GreaterThan(0);
    }
}