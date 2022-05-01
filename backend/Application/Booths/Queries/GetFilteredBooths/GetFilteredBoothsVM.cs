using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Queries.GetFilteredBooths
{
    public class GetFilteredBoothsVM : BoothBaseVM
    {
        public getFilteredBoothsStallVM Stall { get; set; }
    }
}
