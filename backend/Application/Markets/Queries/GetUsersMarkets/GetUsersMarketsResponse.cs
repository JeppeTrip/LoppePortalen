using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetUsersMarkets
{
    public class GetUsersMarketsResponse { 
        public List<UsersMarketsVM> Markets { get; set; }
    }
}
