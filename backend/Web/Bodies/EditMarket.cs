using System;
using System.Collections.Generic;

namespace Web.Bodies
{
    public class EditMarket
    {
        public int MarketId { get; set; }
        public int OrganiserId { get; set; }
        public string MarketName { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public List<(int id, string name, string description, int diff)> StallTypes { get; set; }
    }
}
