using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StallType : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual MarketTemplate MarketTemplate { get; set; }
        public int MarketTemplateId { get; set; }

        public virtual List<Stall> Stalls { get; set; }
    }
}
