using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetFilteredMarkets
{
    public class GetFilteredMarketsQueryRequest
    {
        public bool? HideCancelled { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int? OrganiserId { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public Vector3? DistanceParams { get; set; } //x y coordinate for position to measure from and the z for the distance
    }
}
