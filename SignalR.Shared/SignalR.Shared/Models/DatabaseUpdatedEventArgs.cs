using SignalR.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Shared.Models
{
    public class DatabaseUpdatedEventArgs: EventArgs
    {
        public List<Car> Cars { get; set; }
        public DateTime updateTime { get; set; }=DateTime.Now;
    }
}
