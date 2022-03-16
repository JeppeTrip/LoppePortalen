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
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Organiser Organiser { get; set; }
        public virtual List<MarketInstance> MarketInstances { get; set; }
    }
}
