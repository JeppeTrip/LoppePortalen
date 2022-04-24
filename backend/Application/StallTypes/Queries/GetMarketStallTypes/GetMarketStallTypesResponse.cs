using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Queries.GetMarketStallTypes
{
    public class GetMarketStallTypesResponse
    {
        public List<GetMarketStallTypesVM> StallTypes { get; set; }
    }
}
