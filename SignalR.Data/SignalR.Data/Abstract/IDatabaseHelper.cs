using SignalR.Shared.Entities;
using SignalR.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Data.Abstract
{
    public interface IDatabaseHelper
    {
        public Car updateCarStatus(CarStatus status);
        public int addCarPurchase(int CarID);
        public List<PurchaseList> listCarPurchases();
        public List<Car> listCars();

    }
}
