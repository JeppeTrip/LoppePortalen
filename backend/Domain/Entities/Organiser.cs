using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Organiser : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public virtual List<ContactInfo> ContactInformation { get; set; }
        public virtual List<MarketTemplate> MarketTemplates { get; set; }
        
        public virtual User User { get; set; }
        public string UserId { get; set; }

        public OrganiserImage BannerImage { get; set; }
    }
}
