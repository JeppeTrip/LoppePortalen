using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.EntityExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Booths.Queries.GetFilteredBooths
{
    public class GetFilteredBoothsQuery : IRequest<GetFilteredBoothsResponse>
    {
        public GetFilteredBoothRequest Dto { get; set; }

        public class GetFilteredBoothsQueryHandler : IRequestHandler<GetFilteredBoothsQuery, GetFilteredBoothsResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetFilteredBoothsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetFilteredBoothsResponse> Handle(GetFilteredBoothsQuery request, CancellationToken cancellationToken)
            {
                var startDate = request.Dto.StartDate == null ? DateTimeOffset.MinValue : (DateTimeOffset)request.Dto.StartDate;
                var endDate = request.Dto.EndDate == null ? DateTimeOffset.MaxValue : (DateTimeOffset)request.Dto.EndDate;

                var bookings = _context.Bookings
                    .Include(x => x.Merchant)
                    .Include(x => x.ItemCategories)
                    .Include(x => x.Stall)
                    .ThenInclude(x => x.StallType)
                    .Include(x => x.Stall)
                    .ThenInclude(x => x.MarketInstance)
                    .ThenInclude(x => x.MarketTemplate)
                    .ThenInclude(x => x.Organiser)
                    .Include(x => x.Stall)
                    .ThenInclude(x => x.MarketInstance)
                    .ThenInclude(x => x.Stalls)
                    .ThenInclude(x => x.Bookings);

                //booths on markets that falls on or after startdate
                var filteredBookings = bookings.Where(x => DateTimeOffset.Compare(x.Stall.MarketInstance.StartDate, startDate) >= 0
                || DateTimeOffset.Compare(x.Stall.MarketInstance.EndDate, startDate) >= 0);
                //booths on markets that falls on or before enddate
                filteredBookings = filteredBookings.Where(x => DateTimeOffset.Compare(x.Stall.MarketInstance.StartDate, endDate) <= 0 
                    || DateTimeOffset.Compare(x.Stall.MarketInstance.EndDate, endDate) <= 0);

                var queryResult = await filteredBookings.ToListAsync(cancellationToken);
               
                //if item castegories are sent with, pick out booths with the given item categories otherwise just use all the booths
                if (request.Dto.Categories != null && request.Dto.Categories.Count() > 0)
                    queryResult = queryResult.Where(x => x.ItemCategories.Exists(x => request.Dto.Categories.Contains(x.Name))).ToList();

                //process the filtered list an generate the output list.
                var booths = queryResult.Select(booking => {
                    return new GetFilteredBoothsVM()
                    {
                        Id = booking.Id,
                        Name = booking.BoothName,
                        Description = booking.BoothDescription,
                        Categories = booking.ItemCategories.Select(x => x.Name).ToList(),
                        Stall = new getFilteredBoothsStallVM()
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
                                OccupiedStallCount = booking.Stall.MarketInstance.OccupiedStallCount()
                            }
                        }
                    };
                }).ToList();
                return new GetFilteredBoothsResponse() { Booths = booths };
            }
        }
    }
}
