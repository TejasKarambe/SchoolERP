using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Notifications;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly SchoolDbContext _context;

        public NotificationService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications
                .Include(n => n.CreatedBy)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(int id)
        {
            return await _context.Notifications
                .Include(n => n.CreatedBy)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<List<Notification>> GetByAudienceAsync(string audience)
        {
            return await _context.Notifications
                .Where(n => n.TargetAudience == audience || n.TargetAudience == "All")
                .Where(n => n.IsPublished)
                .OrderByDescending(n => n.PublishedAt)
                .ToListAsync();
        }

        public async Task<Notification> CreateAsync(CreateNotificationDto dto, int userId)
        {
            var entity = new Notification
            {
                Title = dto.Title,
                Body = dto.Body,
                TargetAudience = dto.TargetAudience,
                IsPublished = dto.IsPublished,
                PublishedAt = dto.IsPublished ? DateTime.UtcNow : null,
                CreatedByUserId = userId
            };

            _context.Notifications.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Notification?> UpdateAsync(int id, UpdateNotificationDto dto)
        {
            var entity = await _context.Notifications.FindAsync(id);
            if (entity == null) return null;

            entity.Title = dto.Title;
            entity.Body = dto.Body;
            entity.TargetAudience = dto.TargetAudience;

            if (dto.IsPublished && !entity.IsPublished)
                entity.PublishedAt = DateTime.UtcNow;

            entity.IsPublished = dto.IsPublished;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Notifications.FindAsync(id);
            if (entity == null) return false;

            _context.Notifications.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
