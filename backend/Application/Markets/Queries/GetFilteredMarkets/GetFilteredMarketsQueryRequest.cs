using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetFilteredMarkets
{
    public class GetFilteredMarketsQueryRequest
    {
        //TODO: Expand filter
        public bool? HideCancelled { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int? OrganiserId { get; set; }
        List<string> Categories { get; set; } = new List<string>();

    }
}
