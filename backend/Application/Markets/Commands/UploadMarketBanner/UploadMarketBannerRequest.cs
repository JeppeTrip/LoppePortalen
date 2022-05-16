using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.UploadMarketBanner
{
    public class UploadMarketBannerRequest
    {
        public int MarketId { get; set; }
        public string Title { get; set; }
        public byte[] ImageData { get; set; }
    }
}
