using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Market
    {
        public int MarketId { get; set; }
        public Organiser? Organiser { get; set; }
        public string MarketName { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsCancelled { get; set; }
        public List<StallType> StallTypes { get; set; }
        public List<Stall> Stalls { get; set; }
    }
}
