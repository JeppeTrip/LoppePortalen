using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MarketTemplate : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public int OrganiserId { get; set; }
        public virtual Organiser Organiser { get; set; }
        public virtual List<MarketInstance> MarketInstances { get; set; }

        public virtual List<StallType> StallTypes { get; set; }

        public virtual MarketImage MarketImage { get; set; }
    }
}
