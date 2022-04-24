using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Queries.GetBooth
{
    public class GetBoothVM : BoothBaseVM
    {
        public new GetBoothStallVM Stall { get; set; }
    }
}
