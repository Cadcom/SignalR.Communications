using Microsoft.EntityFrameworkCore;
using SignalR.Shared.Entities;
using System.Reflection.Metadata;

namespace SignalR.Data
{
    public class appContext:DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public appContext(DbContextOptions options ):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    ID=1,
                    CarType = "BMW",
                    isLeftDoorOpen = false,
                    isRightDoorOpen = false,
                   
                }
            );
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    ID = 2,
                    CarType = "Mercedes",
                    isLeftDoorOpen = false,
                    isRightDoorOpen = false,
                }
            );

            modelBuilder.Entity<Purchase>()
        .Property(b => b.ProcessDate)
        .HasDefaultValueSql("getdate()");
        }

    }
}