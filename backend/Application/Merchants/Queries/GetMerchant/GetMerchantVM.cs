using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Queries.GetMerchant
{
    public class GetMerchantVM : MerchantBaseVM
    {
        public List<GetMerchantBoothVM> Booths { get; set; } = new List<GetMerchantBoothVM>();  
        public List<ContactInfoBaseVM> ContactInfo { get; set; } = new List<ContactInfoBaseVM>();
        public string ImageData { get; set; } //Should absolutely be a base64 string.
    }
}
