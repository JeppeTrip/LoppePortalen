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

namespace Application.Merchants.Commands.UploadMerchantBanner
{
    public class UploadMerchantBannerCommand : IRequest<UploadMerchantBannerResponse>
    {
        public UploadMerchantBannerRequest Dto { get; set; }

        public class UploadMerchantBannerCommandHandler : IRequestHandler<UploadMerchantBannerCommand, UploadMerchantBannerResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public UploadMerchantBannerCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<UploadMerchantBannerResponse> Handle(UploadMerchantBannerCommand request, CancellationToken cancellationToken)
            {
                var merchant = await _context.Merchants.FirstOrDefaultAsync(x => x.Id == request.Dto.MerchantId && x.UserId.Equals(_currentUserService.UserId));
                if (merchant == null)
                    throw new NotFoundException("Merchant", request.Dto.MerchantId);

                var image = _context.MerchantImages.FirstOrDefault(x => x.MerchantId == merchant.Id);
                if (image != null)
                {
                    _context.MerchantImages.Remove(image);
                }

                var banner = new MerchantImage() { MerchantId = merchant.Id, ImageTitle = request.Dto.Title, ImageData = request.Dto.ImageData };
                _context.MerchantImages.Add(banner);

                await _context.SaveChangesAsync(cancellationToken);
                return new UploadMerchantBannerResponse() { MerchantId = merchant.Id, Title = request.Dto.Title };
            }
        }
    }
}
