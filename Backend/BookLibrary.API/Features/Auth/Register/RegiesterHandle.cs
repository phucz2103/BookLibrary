using BookLibrary.IRepositories;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookLibrary.API.Features.Auth.Register
{
    public class RegiesterHandle : IRequestHandler<RegisterCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAuthRepository _authRepo;
        public RegiesterHandle(UserManager<User> userManager, IAuthRepository authRepo, RoleManager<Role    > roleManager)
        {
            _userManager = userManager;
            _authRepo = authRepo;
            _roleManager = roleManager;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Address))
                throw new Exception("Vui lòng nhập thông tin bắt buộc!");
            if (request.Username.Length < 6 )
                throw new Exception("Tên tài khoản có ít nhất 6 ký tự!");
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser != null) throw new Exception("Tài khoản đã tồn tại!");
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null) throw new Exception("Email đã được sử dụng!");
            var existingPhone = await _authRepo.GetUserByPhone(request.PhoneNumber);
            if (existingPhone != null) throw new Exception("Số điện thoại đã được sử dụng!");

            var newUser = new User
            {
                UserName = request.Username,
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                CreatedAt = DateTime.Now
            };
            var createUser = await _userManager.CreateAsync(newUser, request.Password);
            if (!createUser.Succeeded) throw new Exception("Đăng ký không thành công, vui lòng thử lại!");
            
            if(!await _roleManager.RoleExistsAsync("User")){
                await _roleManager.CreateAsync(new Role { Name = "User" });
            }

            if (createUser.Succeeded)
            {
                var userRole = await _userManager.AddToRoleAsync(newUser, "User");

                if (userRole.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
