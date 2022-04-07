using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.CreateMarket
{
    public class CreateMarketRequest
    {
        public int OrganiserId { get; set; }
        public string MarketName { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public List<StallDto> Stalls { get; set; }
    }

    public class StallDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }
}
