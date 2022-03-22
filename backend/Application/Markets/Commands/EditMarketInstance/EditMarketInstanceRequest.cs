using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.EditMarketInstance
{
    public class EditMarketInstanceRequest
    {
        public int OrganiserId { get; set; }
        public int MarketInstanceId { get; set;}
        public string MarketName { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
