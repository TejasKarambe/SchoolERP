using SchoolApi.DTOs.Notifications;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<Notification>> GetAllAsync();
        Task<Notification?> GetByIdAsync(int id);
        Task<List<Notification>> GetByAudienceAsync(string audience);
        Task<Notification> CreateAsync(CreateNotificationDto dto, int userId);
        Task<Notification?> UpdateAsync(int id, UpdateNotificationDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
