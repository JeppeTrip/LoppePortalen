using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Queryies.GetMerchant
{
    public class GetMerchantQueryResponse : Result
    {
        public GetMerchantQueryResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public GetMerchantQueryResponse(Result result) : base(result.Succeeded, result.Errors) { }

        public Merchant Merchant { get; set; }
    }
}
