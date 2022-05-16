using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.UploadBanner
{
    public class UploadBannerRequest
    {
        public int OrganiserId { get; set; }
        public string Title { get; set; }
        public byte[] ImageData { get; set; }
    }
}
