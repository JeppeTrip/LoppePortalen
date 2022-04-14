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

namespace Application.Markets.Queries.GetMarket
{
    public class GetMarketInstanceQuery : IRequest<GetMarketInstanceQueryResponse>
    {
        public GetMarketInstanceQueryRequest Dto { get; set; }

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
                    .Include(x => x.MarketTemplate.Organiser)
                    .Include(x => x.MarketTemplate.Organiser.Address)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.MarketId);
                if (marketInstance == null)
                {
                    throw new NotFoundException("No such market.");
                }

                Organiser organiser = new Organiser()
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
                Market market = new Market()
                {
                    MarketId = marketInstance.Id,
                    Description = marketInstance.MarketTemplate.Description,
                    MarketName = marketInstance.MarketTemplate.Name,
                    Organiser = organiser,
                    StartDate = marketInstance.StartDate,
                    EndDate = marketInstance.EndDate,
                    IsCancelled = marketInstance.IsCancelled
                };
                GetMarketInstanceQueryResponse response = new GetMarketInstanceQueryResponse() { 
                    Market = market
                };
                return response;
            }
        }
    }
}
