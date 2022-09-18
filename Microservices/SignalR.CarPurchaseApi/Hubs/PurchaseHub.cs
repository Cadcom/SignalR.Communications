using Microsoft.AspNetCore.SignalR;
using SignalR.Business.Abstract;
using SignalR.Shared.Entities;
using SignalR.Shared.Models;
using System.Text.Json;

namespace SignalR.CarPurchaseApi.Hubs
{
    public class PurchaseHub:Hub
    {
        private readonly IDatabaseService service;

        public PurchaseHub(IDatabaseService service)
        {
            this.service = service;
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            Console.WriteLine(Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }

        async Task sendMessage2Client(string function, string message)
        {
            await Clients.Caller.SendAsync(function, message);
        }

        public void processCar(string status)
        {
            CarStatus? carStatus = JsonSerializer.Deserialize<CarStatus>(status);

            Car result = service.updateCarStatus(carStatus);

            sendMessage2Client("carUpdated", JsonSerializer.Serialize<Car>(result));
        }
    }
}
