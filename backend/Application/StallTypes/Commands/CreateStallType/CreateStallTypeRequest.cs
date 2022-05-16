using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Commands.CreateStallType
{
    public class CreateStallTypeRequest
    {
        public int MarketId { get; set; }  
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
