using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Commands.EditStallTypes
{
    public class EditStallTypeRequest
    {
        public int MarketId { get; set; }
        public int StallTypeId { get; set;}
        public string StallTypeName { get; set; }
        public string StallTypeDescription { get; set; }
    }
}
