using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.CreateStallType
{
    public class CreateStallTypesRequest
    {
        public int MarketId { get; set; }
        public List<(string name, string description)> Types { get; set; }
    }
}
