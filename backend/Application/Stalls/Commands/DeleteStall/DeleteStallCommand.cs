using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Stalls.Commands.DeleteStall
{
    [AuthorizeAttribute(Roles = "ApplicationUser")]
    public class DeleteStallCommand : IRequest<DeleteStallResponse>
    {
        public DeleteStallRequest Dto { get; set; }
        public class DeleteStallCommandHandler : IRequestHandler<DeleteStallCommand, DeleteStallResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public DeleteStallCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<DeleteStallResponse> Handle(DeleteStallCommand request, CancellationToken cancellationToken)
            {
                var stall = await _context.Stalls
                    .Include(x => x.MarketInstance)
                    .ThenInclude(x => x.MarketTemplate)
                    .ThenInclude(x => x.Organiser)
                    .Include(x => x.StallType)
                    .Include(x => x.Bookings)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.StallId && x.MarketInstance.MarketTemplate.Organiser.UserId.Equals(_currentUserService.UserId));

                if (stall == null)
                {
                    throw new NotFoundException($"No stall with id {request.Dto.StallId}.");
                }

                if(stall.MarketInstance.IsCancelled
                    || stall.Bookings.Count() > 0
                    || stall.MarketInstance.StartDate <= DateTimeOffset.Now
                    || stall.MarketInstance.EndDate <= DateTimeOffset.Now)
                {
                    //todo: re evaluate this if there's time?
                    throw new ForbiddenAccessException();
                }

                _context.Stalls.Remove(stall);
                await _context.SaveChangesAsync(cancellationToken);
                return new DeleteStallResponse(Common.Models.Result.Success());
            }
        }
    }
}
