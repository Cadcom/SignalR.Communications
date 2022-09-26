using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using SignalR.CarProcessApi.Hubs;
using SignalR.Data.Abstract;
using SignalR.Shared.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Messages;

namespace SignalR.CarProcessApi.Subscriptions
{
    public interface IDatabaseSubscription
    {
        void Configure(string tableName);
    }

    public class DatabaseSubscription<T> : IDatabaseSubscription where T : class, new()
    {
        SqlTableDependency<T> _tableDependency;
        IConfiguration configuration;
        IHubContext<ProcessHub> _hubContext;

        public DatabaseSubscription(IConfiguration configuration, IHubContext<ProcessHub> hubContext)
        {
            this.configuration = configuration;
            _hubContext = hubContext;
        }

        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<T>(configuration["myConnectionString"], tableName);

            //Veritabanında bir değişiklik olduğu vakit OnChanged event'i tetiklenecektir.
            _tableDependency.OnChanged += async (sender, e) => await _hubContext.Clients.All.SendAsync("databaseRefreshed", "ok");


            //Olası bir hata varsa OnError event'i tetiklenecektir.
            _tableDependency.OnError += (sender, e) => Console.WriteLine("Hata :)");

            //Takibi başlatıyoruz.
            _tableDependency.Start();
        }

        ~DatabaseSubscription() => _tableDependency.Stop();
    }
}
