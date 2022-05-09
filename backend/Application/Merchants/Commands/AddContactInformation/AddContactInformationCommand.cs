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

namespace Application.Merchants.Commands.AddContactInformation
{
    [AuthorizeAttribute(Roles = "ApplicationUser")]
    public class AddContactInformationCommand : IRequest<AddContactInformationResponse>
    {
        public AddContactInformationRequest Dto { get; set; }

        public class AddContactInformationCommandHandler : IRequestHandler<AddContactInformationCommand, AddContactInformationResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public AddContactInformationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<AddContactInformationResponse> Handle(AddContactInformationCommand request, CancellationToken cancellationToken)
            {
                var merchant = await _context.Merchants
                    .Include(x => x.ContactInfo)
                    .FirstOrDefaultAsync(x => x.Id == request.Dto.MerchantId && x.UserId.Equals(_currentUserService.UserId));

                if (merchant == null)
                    throw new NotFoundException("Merchant", request.Dto.MerchantId);

                if (merchant.ContactInfo.Select(x => x.Value).Contains(request.Dto.Value))
                    throw new ValidationException($"Merchant {request.Dto.MerchantId} already have contacts with value {request.Dto.Value}");

                MerchantContactInfo info = new MerchantContactInfo()
                {
                    MerchantId = merchant.Id,
                    ContactType = request.Dto.Type,
                    Value = request.Dto.Value
                };

                _context.MerchantContactInfos.Add(info);
                await _context.SaveChangesAsync(cancellationToken);

                return new AddContactInformationResponse();
            }
        }
    }
}
