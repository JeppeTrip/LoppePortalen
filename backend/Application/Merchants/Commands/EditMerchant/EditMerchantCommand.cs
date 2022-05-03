using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.EditMerchant
{
    [AuthorizeAttribute(Roles = "ApplicationUser")
    public class EditMerchantCommand : IRequest<EditMerchantResponse>
    {
        public EditMerchantRequest Dto { get; set; }

        public class EditMerchantCommandHandler : IRequestHandler<EditMerchantCommand, EditMerchantResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public EditMerchantCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<EditMerchantResponse> Handle(EditMerchantCommand request, CancellationToken cancellationToken)
            {
                var merchant = await _context.Merchants.FirstOrDefaultAsync(x => x.Id == request.Dto.Id && x.UserId.Equals(_currentUserService.UserId));
                if(merchant == null)
                {
                    throw new NotFoundException();
                }

                merchant.Name = request.Dto.Name;
                merchant.Description = request.Dto.Description;
                
                _context.Merchants.Update(merchant);
                await _context.SaveChangesAsync(cancellationToken);
                return new EditMerchantResponse(Result.Success());
            }
        }
    }
}
