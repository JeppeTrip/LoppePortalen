using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.UploadMerchantBanner
{
    public class UploadMerchantBannerRequest
    {
        public int MerchantId { get; set; }
        public string Title { get; set; }
        public byte[] ImageData { get; set; }
    }
}
