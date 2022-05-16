using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Booking : AuditableEntity
    {
        public string Id { get; set; }
        public int MerchantId { get; set; }
        public virtual Merchant Merchant { get; set; }
        public int StallId { get; set; }   
        public virtual Stall Stall { get; set; }
        public string BoothName { get; set; }
        public string BoothDescription { get; set; }
        public List<Category> ItemCategories { get; set; }
        public virtual BookingImage BannerImage { get; set; }
  
    }
}
