using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Queries.GetMerchant
{
    public class GetMerchantStallVM : StallBaseVM
    {
        public MarketBaseVM Market { get; set; }
    }
}
