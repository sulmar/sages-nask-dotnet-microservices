
using Shared.Models;
using Microsoft.AspNetCore.SignalR;
using Ordering.Api.Hubs;
using Bogus;

namespace Ordering.Api.Services;

public class InfoMessageBackgroundService(IHubContext<OrderingHub> hubContext) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var faker = new Faker<Info>()
            .CustomInstantiator(f => new Info(
                 Math.Round( f.Random.Decimal(10_000, 100_000), 0),
                f.Random.Int(10, 100),
                f.Random.Int(100, 200)
                ));

        


        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000);

            var info = faker.Generate();

            await hubContext.Clients.All.SendAsync("ReceiveMessage", info);


        }
    }
}


