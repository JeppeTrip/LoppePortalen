using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.RemoveContactInformation
{
    public class RemoveMerchantContactRequest
    {
        public int MerchantId { get; set; }
        public string Value { get; set; }
    }
}
