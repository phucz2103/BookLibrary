using BookLibrary.API.IRepository;
using BookLibrary.Data;
using Domain.Entities;

namespace BookLibrary.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user;
        }

        public Task<bool> IsEmailExistsAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
