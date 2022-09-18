using SignalR.Shared.Entities;
using SignalR.Shared.Models;
using SignalR.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR.Data.Abstract;

namespace SignalR.Business.Concrete
{
    public class DatabaseService : IDatabaseService
    {
        IDatabaseHelper databaseHelper;
        public DatabaseService(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        public int addCarPurchase(int CarID)
        {
            var result=databaseHelper.addCarPurchase(CarID);

            return result;
        }

        public List<PurchaseList> listCarPurchases()
        {
            return databaseHelper.listCarPurchases();
        }

        public List<Car> listCars()
        {
            return databaseHelper.listCars();
        }

        public Car updateCarStatus(CarStatus status)
        {
            var result = databaseHelper.updateCarStatus(status);
            return result;
        }
    }
}
