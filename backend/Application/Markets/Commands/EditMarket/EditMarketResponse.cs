using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.EditMarket
{
    public class EditMarketResponse : Result
    {
        public EditMarketResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public EditMarketResponse(Result result) : base(result.Succeeded, result.Errors)
        {
        }
    }
}
