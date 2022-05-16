using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Commands.UploadBoothBanner
{
    public class UploadBoothBannerRequest
    {
        public string BoothId { get; set; }
        public string Title { get; set; }
        public byte[] ImageData { get; set; }
    }
}
