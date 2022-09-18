using Microsoft.EntityFrameworkCore;
using SignalR.Data.Abstract;
using SignalR.Shared.Entities;
using SignalR.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Data.Concrate
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private appContext db;

        public DatabaseHelper(appContext db)
        {
            this.db = db;
        }

        public int addCarPurchase(int CarID)
        {
            Purchase purchase = new Purchase() {
                CarID=CarID,
                ProcessDate=DateTime.Now,
            };
            db.Purchases.Add(purchase);
            db.SaveChanges();

            return purchase.ID;
        }

        public List<PurchaseList> listCarPurchases()
        {
            var result = (
                from ca in db.Cars
                join pu in db.Purchases on ca.ID equals pu.CarID

                select new PurchaseList
                {
                    ID = pu.ID,
                    ProcessDate = pu.ProcessDate,
                    Car = ca.CarType,
                });


            return result.ToList();
        }

        public List<Car> listCars()
        {
            return db.Cars.ToList();
        }

        public Car updateCarStatus(CarStatus status)
        {
            Car car = db.Cars.FirstOrDefault (x => x.ID == status.CarID);

            car.isLeftDoorOpen = status.isLeftDoorOpen;
            car.isRightDoorOpen = status.isRightDoorOpen;

            db.Cars.Update(car);
            db.SaveChanges();

            return car;
        }
    }
}
