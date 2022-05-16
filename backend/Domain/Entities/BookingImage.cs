using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookingImage : AuditableEntity
    {
        public string BookingId { get; set; }
        public virtual Booking Booking{ get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
