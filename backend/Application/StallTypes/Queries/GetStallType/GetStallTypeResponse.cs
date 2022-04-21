using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Queries.GetStallType
{
    public class GetStallTypeResponse
    {
        public int StallTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
