using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MarketImage : AuditableEntity
    {
        public int MarketTemplateId { get; set; }
        public virtual MarketTemplate MarketTemplate { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
