using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.EditMerchant
{
    public class EditMerchantResponse : Result
    {
        public EditMerchantResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public EditMerchantResponse(Result result) : base(result.Succeeded, result.Errors) { }
    }
}
