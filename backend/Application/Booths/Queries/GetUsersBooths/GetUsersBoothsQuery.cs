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
                var allBookings = _context.Bookings
                    .Include(x => x.Merchant)
                    .Include(x => x.Stall)
                    .Include(x => x.Stall.StallType)
                    .Include(x => x.Stall.MarketInstance)
                    .Include(x => x.Stall.MarketInstance.MarketTemplate)
                    .Include(x => x.ItemCategories);

                var bookings = await allBookings
                    .Where(x => x.Merchant.UserId.Equals(_currentUserService.UserId))
                    .ToListAsync();

                if(bookings == null || bookings.Count == 0)
                {
                    return new GetUsersBoothsResponse() { Booths = new List<GetUsersBoothsVM>() };
                }


                
                List<Category> itemCategories = new List<Category>();
                List<string> marketCategories = new List<string>();
                var booths = bookings.Select(booking => {
                    itemCategories = allBookings
                        .Where(x => x.Stall.MarketInstanceId == booking.Stall.MarketInstanceId)
                        .SelectMany(x => x.ItemCategories)
                        .ToList();

                    marketCategories = itemCategories.Select(x => x.Name).Distinct().ToList();

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
                                Categories = marketCategories
                            }
                        }
                    };
                }).ToList();

                return new GetUsersBoothsResponse() { Booths = booths };    
            }
        }
    }
}
