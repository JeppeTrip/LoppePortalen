using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Merchants.Queries.GetBooths
{
    public class BoothsDto
    {
        public string BoothName { get; set; }
        public string BoothDescription { get; set; }
        public int StallId { get; set; }
        public int MerchantId { get; set; }
    }
}
