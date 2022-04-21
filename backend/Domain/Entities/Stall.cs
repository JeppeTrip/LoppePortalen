using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Stall : AuditableEntity
    {
        public int Id { get; set; }
        public virtual StallType StallType { get; set; }
        public int StallTypeId { get; set; }

        public virtual List<Booking> Bookings { get; set; }
        public virtual MarketInstance MarketInstance { get; set; }
        public int MarketInstanceId { get; set; }
    } 
}
