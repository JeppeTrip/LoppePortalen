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

namespace Application.Booths.Queries.GetUsersBooths
{
    public class GetUsersBoothsQuery : IRequest<GetUsersBoothsResponse>
    {
        public class GetUsersBoothsQueryHandler : IRequestHandler<GetUsersBoothsQuery, GetUsersBoothsResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public GetUsersBoothsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<GetUsersBoothsResponse> Handle(GetUsersBoothsQuery request, CancellationToken cancellationToken)
            {
                var bookings = await _context.Bookings
                    .Include(x => x.Merchant)
                    .Include(x => x.Stall)
                    .Include(x => x.Stall.StallType)
                    .Include(x => x.Stall.MarketInstance)
                    .Include(x => x.Stall.MarketInstance.MarketTemplate)
                    .Include(x => x.ItemCategories)
                    .Where(x => x.Merchant.UserId.Equals(_currentUserService.UserId))
                    .ToListAsync();

                if(bookings == null || bookings.Count == 0)
                {
                    return new GetUsersBoothsResponse() { Booths = new List<GetUsersBoothsVM>() };
                }

                var booths = bookings.Select(x => new GetUsersBoothsVM()
                {
                    Id = x.Id,
                    Name = x.BoothName,
                    Description = x.BoothDescription,
                    Categories = x.ItemCategories.Select(x => x.Name).ToList(),
                    Stall = new GetUsersBoothsStallVM()
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
                            MarketId = x.Stall.MarketInstanceId,
                            MarketName = x.Stall.MarketInstance.MarketTemplate.Name,
                            Description = x.Stall.MarketInstance.MarketTemplate.Description,
                            StartDate = x.Stall.MarketInstance.StartDate,
                            EndDate = x.Stall.MarketInstance.EndDate,
                            IsCancelled = x.Stall.MarketInstance.IsCancelled
                        }
                    }
                }).ToList();

                return new GetUsersBoothsResponse() { Booths = booths };    
            }
        }
    }
}
