using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.CreateMarket
{
    public class CreateMarketResponse : Result
    {
        public CreateMarketResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public CreateMarketResponse(Result result) : base(result.Succeeded, result.Errors)
        {
        }

        public Market Market { get; set; }
    }
}
