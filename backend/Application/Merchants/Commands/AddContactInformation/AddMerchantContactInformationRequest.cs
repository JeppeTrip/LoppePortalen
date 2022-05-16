using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.AddContactInformation
{
    public class AddMerchantContactInformationRequest
    {
        public int MerchantId { get; set; }
        public string Value { get; set; }
        public ContactInfoType Type { get; set; }
    }
}
