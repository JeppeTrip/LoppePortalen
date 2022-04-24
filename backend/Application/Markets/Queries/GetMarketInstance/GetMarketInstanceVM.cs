using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetMarketInstance
{
    public class GetMarketInstanceVM : MarketBaseVM
    {
        public OrganiserBaseVM Organiser { get; set; }
        public List<StallTypeBaseVM> StallTypes { get; set; }
        public List<BoothBaseVM> Booths { get; set; }
    }
}
