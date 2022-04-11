using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetUsersMarkets
{
    public class GetUsersMarketsResponse : Result
    {
        public GetUsersMarketsResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public GetUsersMarketsResponse(Result result) : base(result.Succeeded, result.Errors)
        {
        }

        public List<Market> Markets { get; set; }
    }
}
