using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Shared.Models
{
    public class CarStatus
    {
        public int CarID { get; set; }
        public bool isLeftDoorOpen { get; set; }
        public bool isRightDoorOpen { get; set; }
    }
}
