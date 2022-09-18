using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR.Business.Abstract;
using SignalR.Shared.Entities;
using SignalR.Shared.Models;
using System.Text.Json;

namespace SignalR.CarProcessApi.Hubs
{
    //[Authorize]
    public class ProcessHub: Hub
    {
        private readonly IDatabaseService service;

        public ProcessHub(IDatabaseService service)
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

        public void processCar(string status) {
            CarStatus? carStatus = JsonSerializer.Deserialize<CarStatus>(status);

            Car result = service.updateCarStatus(carStatus);

            sendMessage2Client("carUpdated", JsonSerializer.Serialize<Car>(result));
        }

        public void purchaseCar(int CarID)
        {
            service.addCarPurchase(CarID);

            sendMessage2Client("carPurchased", CarID.ToString());
        }

        public void listCars()       {

            List<Car> result = service.listCars();

            sendMessage2Client("carsLoaded",JsonSerializer.Serialize<List<Car>>(result));
        }

        public void listPurchaseCars()
        {

            List<PurchaseList> result = service.listCarPurchases();

            sendMessage2Client("purchaseList", JsonSerializer.Serialize<List<PurchaseList>>(result));
        }
    }
}
