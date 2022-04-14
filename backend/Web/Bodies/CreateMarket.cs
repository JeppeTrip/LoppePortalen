using System;
using System.Collections.Generic;

namespace Web.Bodies
{
    public class NewMarketInfo
    {
        public int OrganiserId { get; set; }
        public string MarketName { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public List<(string name, string description, int count)> StallTypes { get; set; }
    }

    
}
