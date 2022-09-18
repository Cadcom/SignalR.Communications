using SignalR.Shared.Entities.baseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Shared.Entities
{
    public class Car: BaseEntity
    {
        
        public string CarType { get; set; }
        public bool isLeftDoorOpen { get; set; }
        public bool isRightDoorOpen { get; set; }
        //public virtual Purchase Purchase { get; set; }
    }
}
