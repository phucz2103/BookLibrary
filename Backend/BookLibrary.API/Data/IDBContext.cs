using Microsoft.EntityFrameworkCore;

namespace BookLibrary.API.Data
{
    public interface IDBContext : IDisposable
    {
        DbSet<TEntity> SetEntity<TEntity>() where TEntity : class;
        Task<int> CommitChangesAsync();
    }
}
