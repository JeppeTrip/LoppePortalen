using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Booking
    {
        public string Id { get; set; }
        public int MerchantId { get; set; }
        public virtual Merchant Merchant { get; set; }
        public int StallId { get; set; }   
        public virtual Stall Stall { get; set; }
        public string BoothName { get; set; }
        public string BoothDescription { get; set; }
    }
}
