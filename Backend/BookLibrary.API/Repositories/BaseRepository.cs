using BookLibrary.API.Data;
using BookLibrary.API.IRepository;
using BookLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.API.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDBContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository( ApplicationDBContext context)
        {
            _context = context;
            _dbSet = _context.SetEntity<TEntity>(); 
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.CommitChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
             _dbSet.Remove(entity);
             return await _context.CommitChangesAsync() > 0;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await  _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await _context.CommitChangesAsync() > 0;
        }
    }
}
