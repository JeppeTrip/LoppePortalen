using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stalls.Commands.RemoveStallsFromMarket
{
    public class RemoveStallsFromMarketResponse : Result
    {
        public RemoveStallsFromMarketResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public RemoveStallsFromMarketResponse(Result result) : base(result.Succeeded, result.Errors)
        {
        }
    }
}
