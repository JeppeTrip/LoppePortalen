using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Merchants.Commands.CreateMerchant
{
    [AuthorizeAttribute(Roles = "ApplicationUser")]
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
                //TODO: Remove since this should be covered by an authorization attribute instead.
                var user = _context.UserInfo.Where(x => x.IdentityId.Equals(_currentUserService.UserId)).FirstOrDefault();
                if(user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var newMerchant = new Merchant() { 
                    Name = request.Dto.Name, 
                    Description = request.Dto.Description, 
                    User= user, 
                    UserId=user.IdentityId 
                };

                _context.Merchants.Add(newMerchant);
                await _context.SaveChangesAsync(cancellationToken);

                return new CreateMerchantResponse() { 
                    Merchant = new MerchantBaseVM { 
                        Id = newMerchant.Id, 
                        Name = newMerchant.Name, 
                        Description = newMerchant.Description, 
                        UserId = newMerchant.UserId } };
            }
        }
    }
}
