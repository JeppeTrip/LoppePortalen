using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
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

namespace Application.Booths.Queries.GetUsersBooths
{
    [AuthorizeAttribute(Roles = "ApplicationUser")]
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
                var allBookings = _context.Bookings
                    .Include(x => x.Merchant)
                    .Include(x => x.Stall)
                    .ThenInclude(x => x.StallType)
                    .Include(x => x.Stall)
                    .ThenInclude(x => x.MarketInstance)
                    .ThenInclude(x => x.MarketTemplate)
                    .Include(x => x.Stall)
                    .ThenInclude(x => x.MarketInstance)
                    .ThenInclude(x => x.Stalls)
                    .ThenInclude(x => x.Bookings)
                    .ThenInclude(x => x.ItemCategories)
                    .Include(x => x.ItemCategories);

                var bookings = await allBookings
                    .Where(x => x.Merchant.UserId.Equals(_currentUserService.UserId))
                    .ToListAsync();

                if(bookings == null || bookings.Count == 0)
                {
                    return new GetUsersBoothsResponse() { Booths = new List<GetUsersBoothsVM>() };
                }


                var booths = bookings.Select(booking => {
                    return new GetUsersBoothsVM() {
                        Id = booking.Id,
                        Name = booking.BoothName,
                        Description = booking.BoothDescription,
                        Categories = booking.ItemCategories.Select(x => x.Name).ToList(),
                        Stall = new GetUsersBoothsStallVM()
                        {
                            Id = booking.Stall.Id,
                            StallType = new StallTypeBaseVM()
                            {
                                Id = booking.Stall.StallType.Id,
                                Name = booking.Stall.StallType.Name,
                                Description = booking.Stall.StallType.Description
                            },
                            Market = new MarketBaseVM()
                            {
                                MarketId = booking.Stall.MarketInstanceId,
                                MarketName = booking.Stall.MarketInstance.MarketTemplate.Name,
                                Description = booking.Stall.MarketInstance.MarketTemplate.Description,
                                StartDate = booking.Stall.MarketInstance.StartDate,
                                EndDate = booking.Stall.MarketInstance.EndDate,
                                IsCancelled = booking.Stall.MarketInstance.IsCancelled,
                                Categories = booking.Stall.MarketInstance.ItemCategories(),
                                TotalStallCount = booking.Stall.MarketInstance.TotalStallCount(),
                                AvailableStallCount = booking.Stall.MarketInstance.AvailableStallCount(),
                                OccupiedStallCount = booking.Stall.MarketInstance.OccupiedStallCount(),
                                Address = booking.Stall.MarketInstance.MarketTemplate.Address,
                                PostalCode = booking.Stall.MarketInstance.MarketTemplate.PostalCode,
                                City = booking.Stall.MarketInstance.MarketTemplate.City
                            }
                        }
                    };
                }).ToList();

                return new GetUsersBoothsResponse() { Booths = booths };    
            }
        }
    }
}
