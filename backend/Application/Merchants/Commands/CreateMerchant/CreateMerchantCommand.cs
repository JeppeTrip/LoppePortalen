using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.CreateMerchant
{
    public class CreateMerchantCommand : IRequest<CreateMerchantResponse>
    {
        public CreateMerchantRequest Dto { get; set; }

        public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, CreateMerchantResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public CreateMerchantCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }
            public async Task<CreateMerchantResponse> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
            {
                var user = _context.UserInfo.Where(x => x.IdentityId.Equals(_currentUserService.UserId)).FirstOrDefault();
                if(user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var newMerchant = new Merchant() { Name = request.Dto.Name, Description = request.Dto.Description, User= user, UserId=user.IdentityId };
                _context.Merchants.Add(newMerchant);
                await _context.SaveChangesAsync(cancellationToken);
                return new CreateMerchantResponse(Common.Models.Result.Success()) { Merchant = new Common.Models.Merchant() { Id = newMerchant.Id, Name = newMerchant.Name, Description = newMerchant.Description, UserId = newMerchant.UserId } };
            }
        }
    }
}
