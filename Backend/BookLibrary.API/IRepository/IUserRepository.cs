using Domain.Entities;

namespace BookLibrary.API.IRepository
{
    public interface IUserRepository
    {
        Task<bool> IsEmailExistsAsync(string email);
        Task AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }
}
