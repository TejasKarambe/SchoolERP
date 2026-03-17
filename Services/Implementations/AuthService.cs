using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Auth;
using SchoolApi.Helpers;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly SchoolDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthService(SchoolDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<TokenResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.IsActive);

            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) return null;

            var accessToken = _jwtHelper.GenerateAccessToken(user);
            var refreshToken = _jwtHelper.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                UserId = user.Id
            };
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == dto.RefreshToken
                    && u.RefreshTokenExpiry > DateTime.UtcNow
                    && u.IsActive);

            if (user == null) return null;

            var accessToken = _jwtHelper.GenerateAccessToken(user);
            var refreshToken = _jwtHelper.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                UserId = user.Id
            };
        }

        public async Task<User> RegisterAsync(RegisterDto dto)
        {
            var entity = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                StaffId = dto.StaffId,
                IsActive = true
            };

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;
            if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.PasswordHash)) return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Staff)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Staff)
                .ToListAsync();
        }

        public async Task<bool> ToggleUserStatusAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsActive = !user.IsActive;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
