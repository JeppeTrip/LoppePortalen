using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Queries.GetMarketStallTypes
{
    public class GetMarketStallTypesVM : StallTypeBaseVM
    {
        public int NumberOfStalls { get; set; }
    }
}
