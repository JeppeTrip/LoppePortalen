using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Queries.AllMerchants
{
    public class AllMerchantsQueryResponse : Result
    {
        public AllMerchantsQueryResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public AllMerchantsQueryResponse(Result result) : base(result.Succeeded, result.Errors) { }

        public List<Merchant> MerchantList { get; set; }
    }
}
