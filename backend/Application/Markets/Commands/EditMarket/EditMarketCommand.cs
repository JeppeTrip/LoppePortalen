using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
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

namespace Application.Markets.Commands.EditMarket
{
    /** Command to update the market information.
        This only covers the details of the market, not the actual stalls and such.
     */
    [AuthorizeAttribute(Roles = "ApplicationUser")]
    public class EditMarketCommand : IRequest<EditMarketResponse>
    {
        public EditMarketRequest Dto { get; set; }

        public class EditMarketCommandHandler : IRequestHandler<EditMarketCommand, EditMarketResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public EditMarketCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<EditMarketResponse> Handle(EditMarketCommand request, CancellationToken cancellationToken)
            {
                var organiser = _context.Organisers.FirstOrDefault(x => x.Id == request.Dto.OrganiserId && x.UserId.Equals(_currentUserService.UserId));
                if (organiser == null)
                {
                    throw new NotFoundException();
                }

                var instance = _context.MarketInstances
                    .Include(x => x.MarketTemplate)
                    .FirstOrDefault(x => x.MarketTemplate.OrganiserId == organiser.Id && x.Id == request.Dto.MarketId);
                if (instance == null)
                {
                    throw new NotFoundException();
                }

                instance.StartDate = request.Dto.StartDate;
                instance.EndDate = request.Dto.EndDate;
                instance.MarketTemplate.Name = request.Dto.MarketName;
                instance.MarketTemplate.Description = request.Dto.Description;

                await _context.SaveChangesAsync(cancellationToken);
                return new EditMarketResponse(Result.Success());
            }
        }

    }
}
