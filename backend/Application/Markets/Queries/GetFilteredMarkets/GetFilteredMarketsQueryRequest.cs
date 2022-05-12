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
        public DistanceParameters DistanceParams { get; set; } //x y coordinate for position to measure from and the z for the distance
    }

    public class DistanceParameters
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Distance { get; set; }

        public DistanceParameters(double X, double Y, double Distance)
        {
            this.X = X;
            this.Y = Y;
            this.Distance = Distance;
        }
    }
}
