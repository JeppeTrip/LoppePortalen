using Application.Common.Exceptions;
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

namespace Application.Booths.Queries.GetBooth
{
    public class GetBoothQuery : IRequest<GetBoothResponse>
    {
        public GetBoothRequest Dto { get; set; }

        public class GetBoothQueryHandler : IRequestHandler<GetBoothQuery, GetBoothResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetBoothQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetBoothResponse> Handle(GetBoothQuery request, CancellationToken cancellationToken)
            {
                var booking = await _context.Bookings
                    .Include(x => x.Stall)
                    .Include(x => x.Stall.StallType)
                    .Include(x => x.Stall.MarketInstance)
                    .Include(x => x.Stall.MarketInstance.MarketTemplate)
                    .Include(x => x.ItemCategories)
                    .FirstOrDefaultAsync(x => x.Id.Equals(request.Dto.Id));
                if (booking == null)
                    throw new NotFoundException($"No booth with id {request.Dto.Id}");

                var vm = new GetBoothVM()
                {
                    Id = booking.Id,
                    Name = booking.BoothName,
                    Description = booking.BoothDescription,
                    Categories = booking.ItemCategories.Select(x => x.Name).ToList(),
                    Stall = new GetBoothStallVM()
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
                            MarketId = booking.Stall.MarketInstance.Id,
                            MarketName = booking.Stall.MarketInstance.MarketTemplate.Name,
                            Description = booking.Stall.MarketInstance.MarketTemplate.Description,
                            StartDate = booking.Stall.MarketInstance.StartDate,
                            EndDate = booking.Stall.MarketInstance.EndDate,
                            IsCancelled = booking.Stall.MarketInstance.IsCancelled
                        }
                    }
                };

                return new GetBoothResponse()
                {
                    Booth = vm
                };
            }
        }
    }
}
