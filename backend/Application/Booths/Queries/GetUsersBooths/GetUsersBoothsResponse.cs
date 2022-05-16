using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Queries.GetUsersBooths
{
    public class GetUsersBoothsResponse
    {
        public List<GetUsersBoothsVM> Booths { get; set; }
    }
}
