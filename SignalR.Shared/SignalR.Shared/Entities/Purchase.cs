using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR.Shared.Entities.baseEntity;

namespace SignalR.Shared.Entities
{
    public class Purchase: BaseEntity
    {
        public Purchase()
        {
            Cars=new List<Car>();
        }
        public DateTime ProcessDate { get; set; } = DateTime.Now;

        public int CarID { get; set; }
        public virtual ICollection<Car> Cars { get; set; }

    }
}
