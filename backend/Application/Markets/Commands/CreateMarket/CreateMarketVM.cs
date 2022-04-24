using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.CreateMarket
{
    public class CreateMarketVM : MarketBaseVM
    {
        public OrganiserBaseVM Organiser { get; set; }
    }
}
