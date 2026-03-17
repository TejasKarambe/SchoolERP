using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.DTOs.Auth;
using SchoolApi.Services.Interfaces;
using System.Security.Claims;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _service.LoginAsync(dto);
            if (result == null) return Unauthorized(new { message = "Invalid email or password." });
            return Ok(result);
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _service.RegisterAsync(dto);
            return Ok(new { message = "User registered successfully.", userId = result.Id });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDto dto)
        {
            var result = await _service.RefreshTokenAsync(dto);
            if (result == null) return Unauthorized(new { message = "Invalid or expired refresh token." });
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userId = int.Parse(User.FindFirstValue("userId") ?? "0");
            await _service.LogoutAsync(userId);
            return Ok(new { message = "Logged out successfully." });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var userId = int.Parse(User.FindFirstValue("userId") ?? "0");
            var user = await _service.GetByIdAsync(userId);
            if (user == null) return NotFound();
            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.Role,
                user.IsActive,
                user.StaffId,
                user.CreatedAt
            });
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var userId = int.Parse(User.FindFirstValue("userId") ?? "0");
            var result = await _service.ChangePasswordAsync(userId, dto);
            if (!result) return BadRequest(new { message = "Old password is incorrect." });
            return Ok(new { message = "Password changed successfully." });
        }

        // ── User Management (Admin Only) ─────────────────────────────────────

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users.Select(u => new
            {
                u.Id,
                u.Username,
                u.Email,
                u.Role,
                u.IsActive,
                u.StaffId,
                u.CreatedAt
            }));
        }

        [HttpPatch("users/{id}/toggle-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleUserStatus(int id)
        {
            var result = await _service.ToggleUserStatusAsync(id);
            if (!result) return NotFound();
            return Ok(new { message = "User status toggled successfully." });
        }
    }
}
