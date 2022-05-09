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

namespace Application.Merchants.Commands.RemoveContactInformation
{
    [AuthorizeAttribute(Roles = "ApplicationUser")]
    public class RemoveMerchantContactCommand : IRequest<RemoveMerchantContactResponse>
    {
        public RemoveMerchantContactRequest Dto { get; set; }

        public class RemoveMerchantContactCommandHandler : IRequestHandler<RemoveMerchantContactCommand, RemoveMerchantContactResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public RemoveMerchantContactCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }
            public async Task<RemoveMerchantContactResponse> Handle(RemoveMerchantContactCommand request, CancellationToken cancellationToken)
            {
                var merchant = await _context.Merchants
                    .Include(x => x.ContactInfo)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.MerchantId && x.UserId.Equals(_currentUserService.UserId));

                if (merchant == null)
                    throw new NotFoundException("Merchant", request.Dto.MerchantId);

                var contact = merchant.ContactInfo.FirstOrDefault(x => x.Value.Equals(request.Dto.Value));
                if (contact == null)
                    throw new NotFoundException($"Merchant {request.Dto.MerchantId} have not contacts matching {request.Dto.Value}");

                _context.MerchantContactInfos.Remove(contact);
                await _context.SaveChangesAsync(cancellationToken);

                return new RemoveMerchantContactResponse();
            }
        }
    }
}
