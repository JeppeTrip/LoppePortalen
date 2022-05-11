using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Booths.Commands.UploadBoothBanner
{
    [AuthorizeAttribute(Roles = "ApplicationUser")]
    public class UploadBoothBannerCommand : IRequest<UploadBoothBannerResponse>
    {
        public UploadBoothBannerRequest Dto { get; set; }

        public class UploadBoothBannerCommandHandler : IRequestHandler<UploadBoothBannerCommand, UploadBoothBannerResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public UploadBoothBannerCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<UploadBoothBannerResponse> Handle(UploadBoothBannerCommand request, CancellationToken cancellationToken)
            {
                var booth = await _context.Bookings
                    .Include(x => x.Merchant)
                    .Include(x => x.BannerImage)
                    .FirstOrDefaultAsync(x => x.Id.Equals(request.Dto.BoothId) && x.Merchant.UserId.Equals(_currentUserService.UserId));

                if (booth == null)
                    throw new NotFoundException("Booth", request.Dto.BoothId);

                var image = _context.BookingImages.FirstOrDefault(x => x.BookingId == booth.Id);
                if (image != null)
                {
                    _context.BookingImages.Remove(image);
                }

                var banner = new BookingImage() { BookingId = booth.Id, ImageTitle = request.Dto.Title, ImageData = request.Dto.ImageData };
                _context.BookingImages.Add(banner);

                await _context.SaveChangesAsync(cancellationToken);
                return new UploadBoothBannerResponse() { BoothId = booth.Id, Title = request.Dto.Title };
            }
        }
    }
}
