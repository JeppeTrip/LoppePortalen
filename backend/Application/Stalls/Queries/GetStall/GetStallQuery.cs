using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.EntityExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Stalls.Queries.GetStall
{
    public class GetStallQuery : IRequest<GetStallResponse>
    { 
        public GetStallRequest Dto { get; set; }

        public class GetStallQueryHandler : IRequestHandler<GetStallQuery, GetStallResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetStallQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetStallResponse> Handle(GetStallQuery request, CancellationToken cancellationToken)
            {
                var stall = await _context.Stalls
                    .Include(x => x.MarketInstance)
                    .ThenInclude(x => x.MarketTemplate)
                    .ThenInclude(x => x.Organiser)
                    .ThenInclude(x => x.Address)
                    .Include(x => x.Bookings)
                    .ThenInclude(x => x.Merchant)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.StallId);

                if (stall == null)
                    throw new NotFoundException($"No stall with Id {request.Dto.StallId}.");

                var vm = new GetStallVM()
                {
                    Id = stall.Id,
                    StallType = new StallTypeBaseVM()
                    {
                        Id = stall.StallType.Id,    
                        Name = stall.StallType.Name,
                        Description = stall.StallType.Description
                    },
                    Market = new GetStallMarketVM()
                    {
                        MarketId = stall.MarketInstance.Id,
                        MarketName = stall.MarketInstance.MarketTemplate.Name,
                        Description = stall.MarketInstance.MarketTemplate.Description,
                        IsCancelled = stall.MarketInstance.IsCancelled,
                        StartDate = stall.MarketInstance.StartDate,
                        EndDate = stall.MarketInstance.EndDate,
                        TotalStallCount = stall.MarketInstance.TotalStallCount(),
                        AvailableStallCount = stall.MarketInstance.AvailableStallCount(),
                        OccupiedStallCount = stall.MarketInstance.OccupiedStallCount(),
                        Categories = stall.MarketInstance.ItemCategories(),
                        Organiser = new OrganiserBaseVM()
                        {
                            Id = stall.MarketInstance.MarketTemplate.Organiser.Id,
                            Name = stall.MarketInstance.MarketTemplate.Organiser.Name,
                            Description = stall.MarketInstance.MarketTemplate.Organiser.Description,
                            UserId = stall.MarketInstance.MarketTemplate.Organiser.UserId,
                            Street = stall.MarketInstance.MarketTemplate.Organiser.Address.Street,
                            StreetNumber = stall.MarketInstance.MarketTemplate.Organiser.Address.Number,
                            Appartment = stall.MarketInstance.MarketTemplate.Organiser.Address.Appartment,
                            City = stall.MarketInstance.MarketTemplate.Organiser.Address.City,
                            PostalCode = stall.MarketInstance.MarketTemplate.Organiser.Address.PostalCode
                        }
                        
                    }
                }

                return new GetStallResponse() { 
                    Stall = vm 
                };
            }
        }
    }
}
