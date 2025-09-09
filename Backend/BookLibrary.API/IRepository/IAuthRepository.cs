using Domain.Entities;

namespace BookLibrary.IRepositories
{
    public interface IAuthRepository
    {
        Task<bool> CreateRefreshToken(RefreshToken refreshToken);
        Task<bool> UpdateRefreshToken(RefreshToken refreshToken);
        Task<bool> DeleteUser(User user);
        Task<bool> BanUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByPhone(string phone);
    }
}