using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;


public interface IApplicationDbContext
{
    public DbSet<Domain.Dog> Dogs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}