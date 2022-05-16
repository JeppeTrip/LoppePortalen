using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Organisers.Queries.GetOrganiser
{
    public class GetOrganiserVM : OrganiserBaseVM
    {
        public List<MarketBaseVM> Markets { get; set; }
        public List<ContactInfoBaseVM> Contacts { get; set; }
        public string ImageData { get; set; } //Should absolutely be a base64 string.
    }
}
