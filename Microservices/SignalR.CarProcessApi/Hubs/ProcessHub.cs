using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SignalR.Business.Abstract;
using SignalR.Shared.Entities;
using SignalR.Shared.Models;
using System.Text.Json;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace SignalR.CarProcessApi.Hubs
{
    //[Authorize]
    public class ProcessHub: Hub
    {
        private readonly IDatabaseService service;
        IConfiguration configuration;
        //IDatabaseSubscription databaseSubscription;

        public ProcessHub(IDatabaseService service, IConfiguration configuration)
        {
            this.service = service;
            //this.databaseSubscription = databaseSubscription;
            //this.databaseSubscription.databaseRefreshed += (o,e)=> sendMessage2Client("databaseRefreshed", "ok");


        }

        private async void SqlTable_OnChanged(object sender, RecordChangedEventArgs<Car> e)
        {
            await sendMessage2Client("databaseRefreshed", "ok");
        }

        //~ProcessHub() => 
        //    _tableDependency.Stop();

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

        public async Task sendMessage2Client(string function, string message)
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
