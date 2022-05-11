using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Markets.Commands.UploadMarketBanner
{
    public class UploadMarketBannerCommand : IRequest<UploadMarketBannerResponse>
    {
        public UploadMarketBannerRequest Dto { get; set; }

        public class UploadMarketBannerCommandHandler : IRequestHandler<UploadMarketBannerCommand, UploadMarketBannerResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public UploadMarketBannerCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<UploadMarketBannerResponse> Handle(UploadMarketBannerCommand request, CancellationToken cancellationToken)
            {
                var marketTemplate = await _context.MarketTemplates
                    .Include(x => x.Organiser)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.MarketId && x.Organiser.UserId.Equals(_currentUserService.UserId));

                if (marketTemplate == null)
                    throw new NotFoundException("Market", request.Dto.MarketId);

                var image = _context.MarketImages.FirstOrDefault(x => x.MarketTemplateId == marketTemplate.Id);
                if (image != null)
                {
                    _context.MarketImages.Remove(image);
                }

                var banner = new MarketImage() { MarketTemplateId = marketTemplate.Id, ImageTitle = request.Dto.Title, ImageData = request.Dto.ImageData };
                _context.MarketImages.Add(banner);

                await _context.SaveChangesAsync(cancellationToken);
                return new UploadMarketBannerResponse() { MarketId = marketTemplate.Id, Title = request.Dto.Title };
            }
        }
    }
}
