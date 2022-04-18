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

        public int? MerchantId { get; set; }
        public virtual Merchant Merchant { get; set; }
    }
}
