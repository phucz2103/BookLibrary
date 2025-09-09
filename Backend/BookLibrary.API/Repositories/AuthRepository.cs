using BookLibrary.Data;
using BookLibrary.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDBContext _context;
        public AuthRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task<bool> BanUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> GetUserByPhone(string phone)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
            return user;
        }

        public Task<bool> UpdateRefreshToken(RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}