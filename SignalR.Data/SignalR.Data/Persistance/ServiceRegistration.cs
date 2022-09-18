using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Data.Persistance
{
    public static class ServiceRegistration
    {
        public static void addPersistanceService(this IServiceCollection services) {
            services.AddDbContext<appContext>(options => options.UseSqlServer("Server=(local);Database=dbCET;Trusted_Connection=True;"));
        }
    }
}
