using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.EntityExtensions;
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
                    .Include(x => x.BannerImage)
                    .Include(o => o.Address)
                    .Include(o => o.ContactInformation)
                    .Include(o => o.MarketTemplates)
                    .ThenInclude(mt => mt.MarketInstances)
                    .ThenInclude(mi => mi.Stalls)
                    .ThenInclude(s => s.StallType)
                    .Include(x => x.MarketTemplates)
                    .ThenInclude(x => x.MarketInstances)
                    .ThenInclude(x => x.Stalls)
                    .ThenInclude(x => x.Bookings)
                    .ThenInclude(x => x.ItemCategories)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.Id);

                if (organiser == null)
                {
                    throw new NotFoundException($"No organiser with ID {request.Dto.Id}");
                }

                List<MarketBaseVM> markets = organiser
                    .MarketTemplates
                    .SelectMany(x => x.MarketInstances)
                    .Select(market => {
                        return new MarketBaseVM()
                        {
                            MarketId = market.Id,
                            MarketName = market.MarketTemplate.Name,
                            Description = market.MarketTemplate.Description,
                            StartDate = market.StartDate,
                            EndDate = market.EndDate,
                            IsCancelled = market.IsCancelled,
                            Categories = market.ItemCategories(),
                            TotalStallCount = market.TotalStallCount(),
                            AvailableStallCount = market.AvailableStallCount(),
                            OccupiedStallCount = market.OccupiedStallCount(),
                            Address = market.MarketTemplate.Address,
                            City = market.MarketTemplate.City,
                            PostalCode = market.MarketTemplate.PostalCode
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
                    Markets = markets,
                    Contacts = organiser.ContactInformation.Select(x => new ContactInfoBaseVM()
                    {
                        Type = x.ContactType,
                        Value = x.Value
                    }).ToList(),
                    ImageData = Convert.ToBase64String(organiser.BannerImage.ImageData)
                };


                return new GetOrganiserQueryResponse() { Organiser = result};
            }
        }
    }
}
