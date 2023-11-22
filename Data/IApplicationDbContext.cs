using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<Student> Students { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    int SaveChanges();
}
