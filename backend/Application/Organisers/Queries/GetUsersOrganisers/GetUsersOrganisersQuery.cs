﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisers.Queries.GetUsersOrganisers
{
    public class GetUsersOrganisersQuery : IRequest<List<GetUsersOrganisersResponse>>
    {
        public GetUsersOrganisersRequest Dto { get; set; }

        public class GetUsersOrganisersQueryHandler : IRequestHandler<GetUsersOrganisersQuery, List<GetUsersOrganisersResponse>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public GetUsersOrganisersQueryHandler(
                IApplicationDbContext context,
                ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }
            public async Task<List<GetUsersOrganisersResponse>> Handle(GetUsersOrganisersQuery request, CancellationToken cancellationToken)
            {
                if (!request.Dto.UserId.Equals(_currentUserService.UserId))
                {
                    throw new UnauthorizedAccessException();
                }

                var organisers = await _context.Organisers
                    .Include(x => x.Address)
                    .Where(x => x.UserId.Equals(request.Dto.UserId))
                    .ToListAsync();

                var result = organisers.Select(x => new GetUsersOrganisersResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Appartment = x.Address.Appartment,
                    City = x.Address.City,
                    Number = x.Address.Number,
                    PostalCode = x.Address.PostalCode,
                    Street = x.Address.Street
                }).OrderBy(x => x.Name).ToList();

                return result;
            }
        }
    }
}
