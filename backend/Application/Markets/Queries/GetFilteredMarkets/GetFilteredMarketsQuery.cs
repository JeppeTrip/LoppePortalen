using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.EntityExtensions;
using Domain.Enums;
using GeoAPI.CoordinateSystems;
using GeoAPI.CoordinateSystems.Transformations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite.Utilities;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Markets.Queries.GetFilteredMarkets
{
    public class GetFilteredMarketsQuery : IRequest<GetFilteredMarketsQueryResponse>    {
        public GetFilteredMarketsQueryRequest Dto { get; set; }

        public class GetFilteredMarketsQueryHandler : IRequestHandler<GetFilteredMarketsQuery, GetFilteredMarketsQueryResponse>
        {
            private readonly int LATITUDE = 1;
            private readonly int LONGITUDE = 0;
            private readonly IApplicationDbContext _context;

            public GetFilteredMarketsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetFilteredMarketsQueryResponse> Handle(GetFilteredMarketsQuery request, CancellationToken cancellationToken)
            {
                List<double> distances;
                List<double> kmDistances;
                var instances = await _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .ThenInclude(x => x.Organiser)
                    .ThenInclude(x => x.Address)
                    .Include(x => x.Stalls)
                    .ThenInclude(x => x.Bookings)
                    .ThenInclude(x => x.ItemCategories)
                    .ToListAsync();

                if (request.Dto.OrganiserId != null)
                {
                    instances = instances.Where(x => x.MarketTemplate.OrganiserId == request.Dto.OrganiserId).ToList();
                }
                if (request.Dto.HideCancelled != null && (bool)request.Dto.HideCancelled)
                {
                    instances = instances.Where(x => !x.IsCancelled).ToList();
                }
                if(request.Dto.DistanceParams != null)
                {
                    distances = instances
                        .Where(x => x.MarketTemplate.Location != null).Select(x => x.MarketTemplate.Location.Distance(new Point(request.Dto.DistanceParams.X, request.Dto.DistanceParams.Y) { SRID = 25832 })).ToList();
                    instances = instances
                        .Where(x => x.MarketTemplate.Location != null)
                        .Where(x => x.MarketTemplate.Location.IsWithinDistance(new Point(request.Dto.DistanceParams.X, request.Dto.DistanceParams.Y) { SRID = 25832 }, request.Dto.DistanceParams.Distance))
                        .ToList();
                    
                }

                var startDate = request.Dto.StartDate == null ? DateTimeOffset.MinValue : (DateTimeOffset) request.Dto.StartDate;
                
                var endDate = request.Dto.EndDate == null ? DateTimeOffset.MaxValue : (DateTimeOffset) request.Dto.EndDate;
                
                instances = instances.Where(
                        x => ( DateTimeOffset.Compare(x.StartDate, startDate) >= 0 || DateTimeOffset.Compare(x.EndDate, startDate) >= 0) )
                    .ToList();

                instances = instances.Where(
                    x => DateTimeOffset.Compare(x.StartDate, endDate) <= 0 || DateTimeOffset.Compare(x.EndDate, endDate) <= 0)
                    .ToList();

                if(request.Dto.Categories.Count > 0)
                    instances = instances.Where(x => x.ItemCategories().Exists(x => request.Dto.Categories.Contains(x))).ToList();

                OrganiserBaseVM organiser;
                var result = instances.Select(market => {
                    organiser = new OrganiserBaseVM
                    {
                        Id = market.MarketTemplate.Organiser.Id,
                        UserId = market.MarketTemplate.Organiser.UserId,
                        Name = market.MarketTemplate.Organiser.Name,
                        Description = market.MarketTemplate.Organiser.Description,
                        Street = market.MarketTemplate.Organiser.Address.Street,
                        StreetNumber = market.MarketTemplate.Organiser.Address.Number,
                        Appartment = market.MarketTemplate.Organiser.Address.Appartment,
                        PostalCode = market.MarketTemplate.Organiser.Address.PostalCode,
                        City = market.MarketTemplate.Organiser.Address.City
                    };
                    return new FilteredMarketVM()
                    {
                        MarketId = market.Id,
                        Description = market.MarketTemplate.Description,
                        MarketName = market.MarketTemplate.Name,
                        Organiser = organiser,
                        StartDate = market.StartDate,
                        EndDate = market.EndDate,
                        IsCancelled = market.IsCancelled,
                        Categories = market.ItemCategories(),
                        TotalStallCount = market.TotalStallCount(),
                        AvailableStallCount = market.AvailableStallCount(),
                        OccupiedStallCount = market.OccupiedStallCount(),
                        Address = market.MarketTemplate.Address,
                        PostalCode = market.MarketTemplate.PostalCode,
                        City = market.MarketTemplate.City
                    };
                }).ToList();

                return new GetFilteredMarketsQueryResponse() { Markets = result};
                
            }
        }
    }
}
