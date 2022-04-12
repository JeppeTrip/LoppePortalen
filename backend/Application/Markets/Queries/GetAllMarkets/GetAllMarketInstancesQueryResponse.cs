using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetAllMarkets
{
    public class GetAllMarketInstancesQueryResponse
    {
        public List<Market> Markets { get; set; }
    }
}
