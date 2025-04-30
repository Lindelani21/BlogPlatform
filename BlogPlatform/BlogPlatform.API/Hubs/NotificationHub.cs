using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BlogPlatform.API.Hubs;

[Authorize]  // Only authenticated users can connect
public class NotificationHub : Hub
{
    private readonly NotificationService _notificationService;

    public NotificationHub(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            // Store the connection
            _notificationService.AddConnection(userId, Context.ConnectionId);

            // Send pending notifications to the newly connected client
            var pendingNotifications = await _notificationService.GetUnreadNotifications(userId);
            await Clients.Caller.SendAsync("ReceivePendingNotifications", pendingNotifications);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            _notificationService.RemoveConnection(userId);
        }
        await base.OnDisconnectedAsync(exception);
    }
}