using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisers.Queries.GetOrganiser
{
    public class GetOrganiserQuery : IRequest<GetOrganiserQueryResponse>
    {
        public GetOrganiserQueryRequest Dto { get; set; }

        public class GetOrganiserQueryHandler : IRequestHandler<GetOrganiserQuery, GetOrganiserQueryResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetOrganiserQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetOrganiserQueryResponse> Handle(GetOrganiserQuery request, CancellationToken cancellationToken)
            {
                var organiser = await _context.Organisers
                    .Include(o => o.Address)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.Id);

                if (organiser == null)
                {
                    throw new NotFoundException($"No organiser with ID {request.Dto.Id}");
                }

                var instances = _context.MarketInstances
                    .Include(m => m.MarketTemplate)
                    .Include(m => m.Stalls);

                var organiserInstances = await instances
                    .Where(m => m.MarketTemplate.OrganiserId == organiser.Id)
                    .ToListAsync();

                var allBookings = _context.Bookings
                    .Include(x => x.Stall)
                    .Include(x => x.ItemCategories);

                List<Category> itemCategories = new List<Category>();
                List<string> marketCategories = new List<string>();
                int total = 0;
                int booked = 0;
                List<MarketBaseVM> markets = organiserInstances.Select(market =>
                {
                    total = market.Stalls.Count();
                    booked = allBookings.Where(b => b.Stall.MarketInstanceId.Equals(market.Id)).Count();

                    itemCategories = allBookings
                        .Where(x => x.Stall.MarketInstanceId == market.Id)
                        .SelectMany(x => x.ItemCategories)
                        .ToList();

                    marketCategories = itemCategories.Select(x => x.Name).Distinct().ToList();

                    return new MarketBaseVM()
                    {
                        MarketId = market.Id,
                        MarketName = market.MarketTemplate.Name,
                        Description = market.MarketTemplate.Description,
                        StartDate = market.StartDate,
                        EndDate = market.EndDate,
                        IsCancelled = market.IsCancelled,
                        AvailableStallCount = total - booked,
                        OccupiedStallCount = booked,
                        TotalStallCount = total,
                        Categories = marketCategories
                    };
                }).ToList();

                GetOrganiserVM result = new GetOrganiserVM()
                {
                    Id = organiser.Id,
                    UserId = organiser.UserId,
                    Name = organiser.Name,
                    Description = organiser.Description,
                    Street = organiser.Address.Street,
                    StreetNumber = organiser.Address.Number,
                    Appartment = organiser.Address.Appartment,
                    PostalCode = organiser.Address.PostalCode,
                    City = organiser.Address.City,
                    Markets = markets
                };


                return new GetOrganiserQueryResponse() { Organiser = result};
            }
        }
    }
}
