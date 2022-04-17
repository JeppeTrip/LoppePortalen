using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.CreateMerchant
{
    public class CreateMerchantRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
