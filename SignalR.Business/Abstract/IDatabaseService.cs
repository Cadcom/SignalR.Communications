using SignalR.Shared.Entities;
using SignalR.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Abstract
{
    public interface IDatabaseService
    {
        public Car updateCarStatus(CarStatus status);
        public int addCarPurchase(int CarID);
        public List<PurchaseList> listCarPurchases();
        public List<Car> listCars();
        //public event EventHandler<DatabaseUpdatedEventArgs> databaseRefreshed;
    }
}
