using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SignalR.Data.Abstract;
using SignalR.Shared.Entities;
using SignalR.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace SignalR.Data.Concrate
{
    public class DatabaseHelper : IDatabaseHelper
    {
        //public event EventHandler<DatabaseUpdatedEventArgs> databaseRefreshed;
        //public delegate void DatabaseUpdatedEventHandler(object sender, DatabaseUpdatedEventArgs e);

        private appContext db;
        IConfiguration configuration;
        //SqlTableDependency<Car> sqlTable;

        public DatabaseHelper(appContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;

            //sqlTable = new SqlTableDependency<Car>(configuration["myConnectionString"], "Cars");

            //sqlTable.OnChanged += SqlTable_OnChanged;
            //sqlTable.Start();
        }

        ~DatabaseHelper() {
            //sqlTable?.Stop();
        }

        //private void SqlTable_OnChanged(object sender, RecordChangedEventArgs<Car> e)
        //{
        //    DatabaseUpdatedEventArgs args = new DatabaseUpdatedEventArgs()
        //    {
        //        updateTime = DateTime.Now,
        //        Cars = null
        //    };
        //    //databaseRefreshed?.Invoke(this, args);
        //}

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
            //SqlConnection sqlConnection =  new SqlConnection(db.Database.GetConnectionString());
            //sqlConnection.Open();
            //using (SqlCommand command = new SqlCommand("SELECT ID,CarType,isLeftDoorOpen,isRightDoorOpen from dbo.Cars", sqlConnection)) 
            //{
                
            //    // Create a dependency and associate it with the SqlCommand.
            //    SqlDependency dependency = new SqlDependency(command);
            //    // Subscribe to the SqlDependency event.
            //    dependency.OnChange += new
            //       OnChangeEventHandler(dbChageNotification);

            //    SqlDependency.Start(db.Database.GetConnectionString());
            //    // Maintain the reference in a class member.

                
            //    command.ExecuteReader();

            //}

            //sqlConnection.Close();

            return db.Cars.ToList();
        }

        //void dbChageNotification(object sender, SqlNotificationEventArgs e) {

        //    var notify = e.ToString();

        //    DatabaseUpdatedEventArgs args = new DatabaseUpdatedEventArgs()
        //    {
        //        updateTime = DateTime.Now,
        //        Cars = null
        //    };
        //    databaseRefreshed?.Invoke(this, args);
        //} 

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
