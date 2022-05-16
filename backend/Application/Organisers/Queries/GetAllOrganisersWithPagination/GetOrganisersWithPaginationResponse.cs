using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Queries.GetAllOrganisersWithPagination
{
    public class GetOrganisersWithPaginationResponse
    {
        public PaginatedList<Organiser> Organisers { get; set; }
    }

    public class Organiser
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
