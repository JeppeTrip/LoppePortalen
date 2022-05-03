using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Queries.GetFilteredBooths
{
    public class GetFilteredBoothRequest
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public int? MerchantId { get; set; }
    }
}
