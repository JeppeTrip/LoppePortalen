using System.Collections.Generic;

namespace Domain.Entities
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public virtual List<Booking> Bookings { get; set; }
        public virtual List<MerchantContactInfo> ContactInfo { get; set; }
        public virtual MerchantImage BannerImage { get; set; }
    }
}
