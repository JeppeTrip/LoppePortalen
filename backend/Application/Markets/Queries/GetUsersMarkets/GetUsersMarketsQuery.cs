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

namespace Application.Markets.Queries.GetUsersMarkets
{
    public class GetUsersMarketsQuery : IRequest<GetUsersMarketsResponse>
    {
        public GetUsersMarketsRequest Dto { get; set; }

        public class GetUsersMarketsQueryHandler : IRequestHandler<GetUsersMarketsQuery, GetUsersMarketsResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public GetUsersMarketsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context =context;
                _currentUserService =currentUserService;
            }

            public async Task<GetUsersMarketsResponse> Handle(GetUsersMarketsQuery request, CancellationToken cancellationToken)
            {
                if(!request.Dto.UserId.Equals(_currentUserService.UserId))
                {
                    throw new UnauthorizedAccessException();
                }

                var marketTemplates = await _context.MarketTemplates
                    .Include(x => x.Organiser)
                    .Where(x => x.Organiser.UserId.Equals(request.Dto.UserId))
                    .Select(x => x.Id)
                    .ToListAsync();

                var instances = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .Where(x => marketTemplates.Contains(x.MarketTemplateId))
                    .Select(x => new Market() {
                        MarketId = x.Id,
                        OrganiserId = x.MarketTemplate.OrganiserId,
                        MarketName = x.MarketTemplate.Name,
                        Description = x.MarketTemplate.Description,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        IsCancelled = x.IsCancelled
                    })
                    .ToListAsync();

                return new GetUsersMarketsResponse(Result.Success()) { Markets = instances };
            }
        }
    }
}
