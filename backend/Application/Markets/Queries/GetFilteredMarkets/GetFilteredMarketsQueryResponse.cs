using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetFilteredMarkets
{
    public class GetFilteredMarketsQueryResponse
    {
        public List<FilteredMarketVM> Markets { get; set; }
    }
}
