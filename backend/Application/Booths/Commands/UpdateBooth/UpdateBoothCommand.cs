using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Booths.Commands.UpdateBooth
{
    public class UpdateBoothCommand : IRequest<UpdateBoothResponse>
    {
        public UpdateBoothRequest Dto { get; set; }

        public class UpdateBoothCommandHandler : IRequestHandler<UpdateBoothCommand, UpdateBoothResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public UpdateBoothCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<UpdateBoothResponse> Handle(UpdateBoothCommand request, CancellationToken cancellationToken)
            {
                var booth = await _context.Bookings
                    .Include(x => x.Merchant)
                    .FirstOrDefaultAsync(x => x.Merchant.UserId.Equals(_currentUserService.UserId) && x.Id.Equals(request.Dto.Id));

                if (booth == null)
                    throw new NotFoundException($"User doesn't have booth with id {request.Dto.Id}");

                booth.BoothName = request.Dto.BoothName;
                booth.BoothDescription = request.Dto.BoothDescription;

                _context.Bookings.Update(booth);
                await _context.SaveChangesAsync(cancellationToken);
                return new UpdateBoothResponse(Result.Success());
            }
        }
    }
}
