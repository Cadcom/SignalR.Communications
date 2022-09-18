using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Shared.Models
{
    public class PurchaseList
    {
        public int ID { get; set; }
        public DateTime ProcessDate { get; set; }
        public string Car { get; set; }
    }
}
