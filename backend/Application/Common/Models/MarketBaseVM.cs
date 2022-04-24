using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class MarketBaseVM
    {
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsCancelled { get; set; }
        public int TotalStallCount { get; set; } = -1;
        public int AvailableStallCount { get; set; } = -1;
        public int OccupiedStallCount { get; set; } = -1;
    }
}
