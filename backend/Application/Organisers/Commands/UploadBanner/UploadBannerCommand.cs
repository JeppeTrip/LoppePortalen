using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.UploadBanner
{
    [AuthorizeAttribute(Roles = "ApplicationUser")]
    public class UploadBannerCommand : IRequest<UploadBannerResponse>
    {
        public UploadBannerRequest Dto { get; set; }

        public class UploadBannerCommandHandler : IRequestHandler<UploadBannerCommand, UploadBannerResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public UploadBannerCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<UploadBannerResponse> Handle(UploadBannerCommand request, CancellationToken cancellationToken)
            {
                var organiser = await _context.Organisers.FirstOrDefaultAsync(x => x.Id == request.Dto.OrganiserId && x.UserId.Equals(_currentUserService.UserId));
                if (organiser == null)
                    throw new NotFoundException("Organiser", request.Dto.OrganiserId);

                var image = _context.OrganiserImages.FirstOrDefault(x => x.OrganiserId == organiser.Id);
                if(image == null)
                {
                    image = new OrganiserImage() { OrganiserId = organiser.Id, ImageTitle = request.Dto.Title, ImageData = request.Dto.ImageData };
                    _context.OrganiserImages.Add(image);
                }
                else
                {
                    image.ImageTitle = request.Dto.Title;
                    image.ImageData = request.Dto.ImageData;
                    _context.OrganiserImages.Update(image);
                }

                await _context.SaveChangesAsync(cancellationToken);
                return new UploadBannerResponse() { OrganiserId = organiser.Id, Title = request.Dto.Title};
            }
        }
    }
}
