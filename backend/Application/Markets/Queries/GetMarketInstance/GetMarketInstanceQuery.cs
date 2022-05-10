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

namespace Application.Markets.Queries.GetMarketInstance
{
    public class GetMarketInstanceQuery : IRequest<GetMarketInstanceQueryResponse>
    {
        public GetMarketInstanceRequest Dto { get; set; }

        public class GetMarketInstanceQueryHandler : IRequestHandler<GetMarketInstanceQuery, GetMarketInstanceQueryResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetMarketInstanceQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<GetMarketInstanceQueryResponse> Handle(GetMarketInstanceQuery request, CancellationToken cancellationToken)
            {
                var marketInstance = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .ThenInclude(x => x.StallTypes)
                    .Include(x => x.MarketTemplate.Organiser)
                    .ThenInclude(x => x.Address)
                    .Include(x => x.Stalls)
                    .ThenInclude(x => x.Bookings)
                    .ThenInclude(x => x.ItemCategories)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.MarketId);
                if (marketInstance == null)
                {
                    throw new NotFoundException($"No market with id {request.Dto.MarketId}.");
                }

                OrganiserBaseVM organiser = new OrganiserBaseVM()
                {
                    Id = marketInstance.MarketTemplate.Organiser.Id,
                    UserId = marketInstance.MarketTemplate.Organiser.UserId,
                    Name = marketInstance.MarketTemplate.Organiser.Name,
                    Description = marketInstance.MarketTemplate.Organiser.Description,
                    Street = marketInstance.MarketTemplate.Organiser.Address.Street,
                    StreetNumber = marketInstance.MarketTemplate.Organiser.Address.Number,
                    Appartment = marketInstance.MarketTemplate.Organiser.Address.Appartment,
                    PostalCode = marketInstance.MarketTemplate.Organiser.Address.PostalCode,
                    City = marketInstance.MarketTemplate.Organiser.Address.City
                };

                GetMarketInstanceVM market = new GetMarketInstanceVM()
                {
                    MarketId = marketInstance.Id,
                    Description = marketInstance.MarketTemplate.Description,
                    MarketName = marketInstance.MarketTemplate.Name,
                    Organiser = organiser,
                    StartDate = marketInstance.StartDate,
                    EndDate = marketInstance.EndDate,
                    Address = marketInstance.MarketTemplate.Address,
                    PostalCode= marketInstance.MarketTemplate.PostalCode,
                    City = marketInstance.MarketTemplate.City,
                    IsCancelled = marketInstance.IsCancelled,
                    Categories = marketInstance.ItemCategories(),
                    TotalStallCount = marketInstance.TotalStallCount(),
                    AvailableStallCount = marketInstance.AvailableStallCount(),
                    OccupiedStallCount = marketInstance.OccupiedStallCount(),
                    //Construct stall type vms to send with the market info.
                    StallTypes = marketInstance.MarketTemplate.StallTypes.Select(x => new StallTypeBaseVM()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    }).ToList(),
                    //Construct the stall VM
                    Stalls = marketInstance.Stalls.Select(x => new StallBaseVM()
                        {
                            Id = x.Id,
                            StallType = new StallTypeBaseVM()
                            {
                                Id = x.StallTypeId,
                                Name = x.StallType.Name,
                                Description = x.StallType.Description
                            }
                        }).ToList(),
                    //Construct booth vms to send with the market info.
                    Booths = marketInstance.Stalls
                        .Where(x => x.Bookings.Count() > 0)
                        .SelectMany(x => x.Bookings)
                        .Select(x => new BoothBaseVM()
                            {
                                Id =x.Id,
                                Name = x.BoothName,
                                Description = x.BoothDescription,
                                Categories = x.ItemCategories.Select(x => x.Name).ToList(),
                                Stall = new StallBaseVM()
                                {
                                    Id = x.StallId,
                                    StallType = new StallTypeBaseVM()
                                    {
                                        Id = x.Stall.StallTypeId,
                                        Name = x.Stall.StallType.Name,
                                        Description = x.Stall.StallType.Description
                                    }
                                }
                            }).ToList()
                };

                GetMarketInstanceQueryResponse response = new GetMarketInstanceQueryResponse()
                {
                    Market = market
                };
                return response;
            }
        }
    }
}
