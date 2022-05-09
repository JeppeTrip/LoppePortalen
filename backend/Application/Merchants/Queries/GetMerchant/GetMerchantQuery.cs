using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.EntityExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Merchants.Queries.GetMerchant
{
    public class GetMerchantQuery : IRequest<GetMerchantQueryResponse>
    {
        public GetMerchantQueryRequest Dto { get; set; }

        public class GetMerchantQueryHandler : IRequestHandler<GetMerchantQuery, GetMerchantQueryResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetMerchantQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<GetMerchantQueryResponse> Handle(GetMerchantQuery request, CancellationToken cancellationToken)
            {
                var merchant = await _context.Merchants
                    .Include(x => x.ContactInfo)
                    .Include(x => x.Bookings)
                    .ThenInclude(x => x.Stall)
                    .ThenInclude(x => x.StallType)
                    .Include(x => x.Bookings)
                    .ThenInclude(x => x.Stall)
                    .ThenInclude(x => x.MarketInstance)
                    .ThenInclude(x => x.Stalls)
                    .ThenInclude(x => x.Bookings)
                    .ThenInclude(x => x.ItemCategories)
                    .Include(x => x.Bookings)
                    .ThenInclude(x => x.Stall)
                    .ThenInclude(x => x.MarketInstance)
                    .ThenInclude(x => x.MarketTemplate)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.Id);
                
                if (merchant == null)
                {
                    throw new NotFoundException($"No merchant with ID {request.Dto.Id}.");
                }

                var merchantVm = new GetMerchantVM()
                {
                    Id = merchant.Id,
                    Name = merchant.Name,
                    Description = merchant.Description,
                    UserId = merchant.UserId,
                    //only create view models for booths that are on markets that aren't done
                    Booths = merchant.Bookings
                                .Where(x => !x.Stall.MarketInstance.IsCancelled)
                                .Where(x => DateTimeOffset.Compare(x.Stall.MarketInstance.StartDate, DateTimeOffset.Now) >= 0
                                        || DateTimeOffset.Compare(x.Stall.MarketInstance.EndDate, DateTimeOffset.Now) >= 0)
                                .Select(x => new GetMerchantBoothVM()
                    {
                          Id = x.Id,
                          Name = x.BoothName,
                          Description = x.BoothDescription,
                          Categories = x.ItemCategories.Select(x => x.Name).ToList(),
                          Stall = new GetMerchantStallVM()
                          {
                              Id = x.Stall.Id,
                              StallType = new StallTypeBaseVM()
                              {
                                  Id = x.Stall.StallType.Id,
                                  Name = x.Stall.StallType.Name,
                                  Description = x.Stall.StallType.Description
                              },
                              Market = new MarketBaseVM()
                              {
                                  MarketId = x.Stall.MarketInstance.Id,
                                  Description = x.Stall.MarketInstance.MarketTemplate.Description,
                                  MarketName = x.Stall.MarketInstance.MarketTemplate.Name,
                                  StartDate = x.Stall.MarketInstance.StartDate,
                                  EndDate = x.Stall.MarketInstance.EndDate,
                                  IsCancelled = x.Stall.MarketInstance.IsCancelled,
                                  Categories = x.Stall.MarketInstance.ItemCategories(),
                                  TotalStallCount = x.Stall.MarketInstance.TotalStallCount(),
                                  AvailableStallCount = x.Stall.MarketInstance.AvailableStallCount(),
                                  OccupiedStallCount = x.Stall.MarketInstance.OccupiedStallCount(),
                              }
                          }
                    }).ToList(),
                    ContactInfo = merchant.ContactInfo.Select(x => new ContactInfoBaseVM()
                    {
                        Type = x.ContactType,
                        Value = x.Value
                    }).ToList()
                };

                return new GetMerchantQueryResponse() { 
                    Merchant = merchantVm
                };
            }
        }
    }
}
