using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.CreateMerchant
{
    public class CreateMerchantResponse : Result
    {
        public CreateMerchantResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public CreateMerchantResponse(Result result) : base(result.Succeeded, result.Errors) { }

        public Merchant Merchant { get; set; }
    }
}
