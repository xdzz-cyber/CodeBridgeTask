using System.Linq.Expressions;
using Application.Common.Exceptions;
using Application.Dog.Commands.CreateDog;
using Application.Dog.Queries;
using Application.Interfaces;
using Bogus;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using Persistence;

namespace TestProject1;

public class UnitTest1
{
    // Write test using Moq and Bogus to test getting all dogs
    [Fact]
    public async Task GetAllDogs_Success()
    {
        // Arrange
        var mock = new Mock<IApplicationDbContext>();
        var faker = new Faker();

        var dogs = new List<Domain.Dog>()
        {
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = faker.Random.String2(10),
                TailLength = faker.Random.Int(1, 100),
                Weight = faker.Random.Int(1, 100)
            },
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = faker.Random.String2(10),
                TailLength = faker.Random.Int(1, 100),
                Weight = faker.Random.Int(1, 100)
            }
        };

        mock.Setup(m => m.Dogs).ReturnsDbSet(dogs);

        var context = mock.Object;
        var handler = new GetDogsHandler(context);

        // Act
        var result = await handler.Handle(new GetDogs(), CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count);
    }
    
    // Write test to check if the dogs are sorted by weight in ascending order
    [Fact]
    public async Task GetAllDogs_SortByWeightAscending_Success()
    {
        // Arrange
        var mock = new Mock<IApplicationDbContext>();
        var faker = new Faker();

        var dogs = new List<Domain.Dog>()
        {
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = faker.Random.String2(10),
                TailLength = faker.Random.Int(1, 100),
                Weight = 10
            },
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = faker.Random.String2(10),
                TailLength = faker.Random.Int(1, 100),
                Weight = 5
            }
        };
        
        dogs = dogs.OrderBy(d => d.Weight).ToList();

        mock.Setup(m => m.Dogs).ReturnsDbSet(dogs);

        var context = mock.Object;
        var handler = new GetDogsHandler(context);

        // Act
        var result = await handler.Handle(new GetDogs(), CancellationToken.None);

        // Assert
        Assert.Equal(5, result[0].Weight);
        Assert.Equal(10, result[1].Weight);
    }
    
    // Write test to check if the dogs are sorted by weight in descending order
    [Fact]
    public async Task GetAllDogs_SortByWeightDescending_Success()
    {
        // Arrange
        var mock = new Mock<IApplicationDbContext>();
        var faker = new Faker();

        var dogs = new List<Domain.Dog>()
        {
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = faker.Random.String2(10),
                TailLength = faker.Random.Int(1, 100),
                Weight = 10
            },
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = faker.Random.String2(10),
                TailLength = faker.Random.Int(1, 100),
                Weight = 5
            }
        };
        
        dogs = dogs.OrderByDescending(d => d.Weight).ToList();

        mock.Setup(m => m.Dogs).ReturnsDbSet(dogs);

        var context = mock.Object;
        var handler = new GetDogsHandler(context);

        // Act
        var result = await handler.Handle(new GetDogs(), CancellationToken.None);

        // Assert
        Assert.Equal(10, result[0].Weight);
        Assert.Equal(5, result[1].Weight);
    }
    
    // Write test to check if the dogs are sorted by tail length in ascending order
    [Fact]
    public async Task GetAllDogs_SortByTailLengthAscending_Success()
    {
        // Arrange
        var mock = new Mock<IApplicationDbContext>();
        var faker = new Faker();

        var dogs = new List<Domain.Dog>()
        {
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = faker.Random.String2(10),
                TailLength = 10,
                Weight = faker.Random.Int(1, 100)
            },
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = faker.Random.String2(10),
                TailLength = 5,
                Weight = faker.Random.Int(1, 100)
            }
        };
        
        dogs = dogs.OrderBy(d => d.TailLength).ToList();

        mock.Setup(m => m.Dogs).ReturnsDbSet(dogs);

        var context = mock.Object;
        var handler = new GetDogsHandler(context);

        // Act
        var result = await handler.Handle(new GetDogs(), CancellationToken.None);

        // Assert
        Assert.Equal(5, result[0].TailLength);
        Assert.Equal(10, result[1].TailLength);
    }
    
    // Write test to check if the dogs are sorted by tail length in descending order
    [Fact]
    public async Task GetAllDogs_SortByTailLengthDescending_Success()
    {
        // Arrange
        var mock = new Mock<IApplicationDbContext>();
        var faker = new Faker();

        var dogs = new List<Domain.Dog>()
        {
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = faker.Random.String2(10),
                TailLength = 10,
                Weight = faker.Random.Int(1, 100)
            },
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = faker.Random.String2(10),
                TailLength = 5,
                Weight = faker.Random.Int(1, 100)
            }
        };
        
        dogs = dogs.OrderByDescending(d => d.TailLength).ToList();

        mock.Setup(m => m.Dogs).ReturnsDbSet(dogs);

        var context = mock.Object;
        var handler = new GetDogsHandler(context);

        // Act
        var result = await handler.Handle(new GetDogs(), CancellationToken.None);

        // Assert
        Assert.Equal(10, result[0].TailLength);
        Assert.Equal(5, result[1].TailLength);
    }
    
    // Write test to check if the dogs are sorted by name in ascending order
    [Fact]
    public async Task GetAllDogs_SortByNameAscending_Success()
    {
        // Arrange
        var mock = new Mock<IApplicationDbContext>();
        var faker = new Faker();

        var dogs = new List<Domain.Dog>()
        {
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = "B",
                Color = faker.Random.String2(10),
                TailLength = faker.Random.Int(1, 100),
                Weight = faker.Random.Int(1, 100)
            },
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = "A",
                Color = faker.Random.String2(10),
                TailLength = faker.Random.Int(1, 100),
                Weight = faker.Random.Int(1, 100)
            }
        };
        
        dogs = dogs.OrderBy(d => d.Name).ToList();

        mock.Setup(m => m.Dogs).ReturnsDbSet(dogs);

        var context = mock.Object;
        var handler = new GetDogsHandler(context);

        // Act
        var result = await handler.Handle(new GetDogs(), CancellationToken.None);

        // Assert
        Assert.Equal("A", result[0].Name);
        Assert.Equal("B", result[1].Name);
    }
    
    // Write test to check if the dogs are sorted by name in descending order
    [Fact]
    public async Task GetAllDogs_SortByNameDescending_Success()
    {
        // Arrange
        var mock = new Mock<IApplicationDbContext>();
        var faker = new Faker();

        var dogs = new List<Domain.Dog>()
        {
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = "B",
                Color = faker.Random.String2(10),
                TailLength = faker.Random.Int(1, 100),
                Weight = faker.Random.Int(1, 100)
            },
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = "A",
                Color = faker.Random.String2(10),
                TailLength = faker.Random.Int(1, 100),
                Weight = faker.Random.Int(1, 100)
            }
        };
        
        dogs = dogs.OrderByDescending(d => d.Name).ToList();

        mock.Setup(m => m.Dogs).ReturnsDbSet(dogs);

        var context = mock.Object;
        var handler = new GetDogsHandler(context);

        // Act
        var result = await handler.Handle(new GetDogs(), CancellationToken.None);

        // Assert
        Assert.Equal("B", result[0].Name);
        Assert.Equal("A", result[1].Name);
    }
    
    // Write test to check if the dogs are sorted by color in ascending order
    [Fact]
    public async Task GetAllDogs_SortByColorAscending_Success()
    {
        // Arrange
        var mock = new Mock<IApplicationDbContext>();
        var faker = new Faker();

        var dogs = new List<Domain.Dog>()
        {
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = "B",
                TailLength = faker.Random.Int(1, 100),
                Weight = faker.Random.Int(1, 100)
            },
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = "A",
                TailLength = faker.Random.Int(1, 100),
                Weight = faker.Random.Int(1, 100)
            }
        };
        
        dogs = dogs.OrderBy(d => d.Color).ToList();

        mock.Setup(m => m.Dogs).ReturnsDbSet(dogs);

        var context = mock.Object;
        var handler = new GetDogsHandler(context);

        // Act
        var result = await handler.Handle(new GetDogs(), CancellationToken.None);

        // Assert
        Assert.Equal("A", result[0].Color);
        Assert.Equal("B", result[1].Color);
    }
    
    // Write test to check if the dogs are sorted by color in descending order
    [Fact]
    public async Task GetAllDogs_SortByColorDescending_Success()
    {
        // Arrange
        var mock = new Mock<IApplicationDbContext>();
        var faker = new Faker();

        var dogs = new List<Domain.Dog>()
        {
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = "B",
                TailLength = faker.Random.Int(1, 100),
                Weight = faker.Random.Int(1, 100)
            },
            new Domain.Dog()
            {
                Id = faker.Random.Guid(),
                Name = faker.Random.String2(10),
                Color = "A",
                TailLength = faker.Random.Int(1, 100),
                Weight = faker.Random.Int(1, 100)
            }
        };
        
        dogs = dogs.OrderByDescending(d => d.Color).ToList();

        mock.Setup(m => m.Dogs).ReturnsDbSet(dogs);

        var context = mock.Object;
        var handler = new GetDogsHandler(context);

        // Act
        var result = await handler.Handle(new GetDogs(), CancellationToken.None);

        // Assert
        Assert.Equal("B", result[0].Color);
        Assert.Equal("A", result[1].Color);
    }
    
    // Write test to check if the creation of dog fails because dog already exists
    [Fact]
    public async Task CreateDog_DogAlreadyExists_Failure()
    {
        // Arrange
        var mock = new Mock<IApplicationDbContext>();
        var faker = new Faker();

        var dog = new Domain.Dog()
        {
            Id = faker.Random.Guid(),
            Name = faker.Random.String2(10),
            Color = faker.Random.String2(10),
            TailLength = faker.Random.Int(1, 100),
            Weight = faker.Random.Int(1, 100)
        };

        mock.Setup(m => m.Dogs.Add(dog))
            .Throws(new NotSupportedException("Dog already exists"));

        var context = mock.Object;
        var handler = new CreateDogHandler(context);

        // Act and Assert
        await Assert.ThrowsAsync<NotSupportedException>(async () =>
        {
            await handler.Handle(new CreateDog(dog.Name, dog.Color, dog.TailLength, dog.Weight),
                CancellationToken.None);
        });
    }

}