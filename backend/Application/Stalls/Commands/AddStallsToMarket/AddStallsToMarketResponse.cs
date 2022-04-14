using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stalls.Commands.AddStallsToMarket
{
    public class AddStallsToMarketResponse : Result
    {
        public List<Stall> Stalls { get; set; }

        public AddStallsToMarketResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public AddStallsToMarketResponse(Result result) : base(result.Succeeded, result.Errors)
        {
        }
    }
}
