using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisers.Queries.GetAllOrganisers
{
    public class GetAllOrganisersQuery : IRequest<GetAllOrganisersResponse>
    {
        public class GetAllOrganisersQueryHandler : IRequestHandler<GetAllOrganisersQuery, GetAllOrganisersResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetAllOrganisersQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetAllOrganisersResponse> Handle(GetAllOrganisersQuery request, CancellationToken cancellationToken)
            {
                var allOrganisers = await _context.Organisers
                    .Select(x => new Organiser() { 
                        Id = x.Id, 
                        UserId = x.UserId,
                        Name=x.Name,
                        Description = x.Description,
                        Street = x.Address.Street,
                        StreetNumber = x.Address.Number,
                        Appartment = x.Address.Appartment,
                        PostalCode = x.Address.PostalCode,
                        City = x.Address.City
                    })
                    .ToListAsync(); 

                return new GetAllOrganisersResponse() { Organisers = allOrganisers};
            }
        }
    }
}
