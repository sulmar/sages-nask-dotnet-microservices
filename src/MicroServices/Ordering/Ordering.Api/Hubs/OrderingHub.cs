using Microsoft.AspNetCore.SignalR;

namespace Ordering.Api.Hubs;

public class OrderingHub(ILogger<OrderingHub> logger) : Hub
{
    public override Task OnConnectedAsync()
    {
        logger.LogInformation("Connected {ConnectionId}", Context.ConnectionId);         

        return base.OnConnectedAsync();
    }

 
}
