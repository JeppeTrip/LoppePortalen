using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Queries.GetUsersMerchants
{
    public class GetUsersMerchantsResponse
    { 
        public List<MerchantBaseVM> Merchants { get; set; }
    }
}
