using BlogPlatform.API.Hubs;
using BlogPlatform.Core.Models;
using BlogPlatform.Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.API.Services;

public class NotificationService
{
    private readonly BlogContext _context;
    private readonly Dictionary<string, List<string>> _userConnections = new();

    public NotificationService(BlogContext context)
    {
        _context = context;
    }

    // Track user connections
    public void AddConnection(string userId, string connectionId)
    {
        lock (_userConnections)
        {
            if (!_userConnections.ContainsKey(userId))
            {
                _userConnections[userId] = new List<string>();
            }
            _userConnections[userId].Add(connectionId);
        }
    }

    public void RemoveConnection(string userId)
    {
        lock (_userConnections)
        {
            _userConnections.Remove(userId);
        }
    }

    public List<string> GetUserConnections(string userId)
    {
        lock (_userConnections)
        {
            return _userConnections.TryGetValue(userId, out var connections)
                ? connections
                : new List<string>();
        }
    }

    // Database operations
    public async Task<List<Notification>> GetUnreadNotifications(string userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task CreateAndSendNotification(
        string userId,
        string message,
        IHubContext<NotificationHub> hubContext)
    {
        // Save to database
        var notification = new Notification
        {
            Message = message,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        // Send via SignalR if user is online
        var connections = GetUserConnections(userId);
        if (connections.Any())
        {
            await hubContext.Clients.Clients(connections)
                .SendAsync("ReceiveNotification", new
                {
                    Id = notification.Id,
                    Message = notification.Message,
                    CreatedAt = notification.CreatedAt
                });
        }
    }
}