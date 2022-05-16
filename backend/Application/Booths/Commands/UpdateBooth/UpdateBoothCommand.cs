using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Booths.Commands.UpdateBooth
{
    [AuthorizeAttribute(Roles = "ApplicationUser")]
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
                    .Include(x => x.ItemCategories)
                    .FirstOrDefaultAsync(x => x.Merchant.UserId.Equals(_currentUserService.UserId) && x.Id.Equals(request.Dto.Id));

                if (booth == null)
                    throw new NotFoundException($"User doesn't have booth with id {request.Dto.Id}");

                var validCategories = await _context.ItemCategories.ToListAsync();
                request.Dto.ItemCategories.ForEach(x =>
                {
                    if (validCategories.FirstOrDefault(cat => cat.Name.Equals(x)) == null)
                        throw new ValidationException($"Category with name {x} is invalid.");

                    if (booth.ItemCategories.FirstOrDefault(cat => cat.Name.Equals(x)) == null)
                        booth.ItemCategories.Add(validCategories.First(cat => cat.Name.Equals(x)));
                });

                booth.ItemCategories.RemoveAll(x => !(request.Dto.ItemCategories.Contains(x.Name)));

                booth.BoothName = request.Dto.BoothName;
                booth.BoothDescription = request.Dto.BoothDescription;

                _context.Bookings.Update(booth);
                await _context.SaveChangesAsync(cancellationToken);
                return new UpdateBoothResponse(Result.Success());
            }
        }
    }
}
