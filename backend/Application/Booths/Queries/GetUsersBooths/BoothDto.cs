using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Queries.GetUsersBooths
{
    public class BoothDto
    {
        public string Id { get; set; }
        public string BoothName { get; set; }
        public string BoothDescription { get; set; }
        public int StallId { get; set; }
        public int MerchantId { get; set; }
    }
}
