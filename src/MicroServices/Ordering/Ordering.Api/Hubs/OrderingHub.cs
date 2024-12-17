using Microsoft.AspNetCore.SignalR;

namespace Ordering.Api.Hubs;

public class OrderingHub(ILogger<OrderingHub> logger) : Hub
{
    public override Task OnConnectedAsync()
    {
        logger.LogInformation("Connected {ConnectionId}", Context.ConnectionId);

        var group = this.Context.User.Claims.FirstOrDefault(p => p.Type == "Group");

        if (group != null)
        {
            this.Groups.AddToGroupAsync(Context.ConnectionId, group.Value);
        }

        return base.OnConnectedAsync();
    }

 
}
