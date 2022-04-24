using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.CreateMerchant
{
    public class CreateMerchantResponse
    {
        public MerchantBaseVM Merchant { get; set; }
    }
}
