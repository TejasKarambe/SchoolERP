using SchoolApi.DTOs.Auth;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> LoginAsync(LoginDto dto);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenDto dto);
        Task<User> RegisterAsync(RegisterDto dto);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto);
        Task<bool> LogoutAsync(int userId);
        Task<User?> GetByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> ToggleUserStatusAsync(int userId);
    }
}
