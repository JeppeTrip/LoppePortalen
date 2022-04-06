using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Stall
    {
        int Id { get; set; }

        public virtual MarketTemplate MarketTemplate { get; set; }
        public int MarketTemplateId { get; set; }
    }
}
