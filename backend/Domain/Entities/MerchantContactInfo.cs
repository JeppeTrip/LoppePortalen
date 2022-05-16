using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MerchantContactInfo
    {
        public ContactInfoType ContactType { get; set; }
        public string Value { get; set; }
        public int MerchantId { get; set; }
        public virtual Merchant Merchant { get; set; }
    }
}
