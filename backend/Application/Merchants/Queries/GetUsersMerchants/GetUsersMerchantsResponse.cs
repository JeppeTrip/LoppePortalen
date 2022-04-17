using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Queries.GetUsersMerchants
{
    public class GetUsersMerchantsResponse : Result
    {
        public GetUsersMerchantsResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public GetUsersMerchantsResponse(Result result) : base(result.Succeeded, result.Errors) { }

        public List<Merchant> Merchants { get; set; }
    }
}
