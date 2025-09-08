using Domain.Entities;

namespace BookLibrary.IRepositories
{
    public interface IAuthRepository
    {
        Task<bool> CreateRefreshToken(RefreshToken refreshToken);
        Task<bool> UpdateRefreshToken(RefreshToken refreshToken);
        Task<bool> CreateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> BanUser(User user);
    }
}