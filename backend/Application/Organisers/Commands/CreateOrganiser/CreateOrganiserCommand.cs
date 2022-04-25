using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.CreateOrganiser
{
    public class CreateOrganiserCommand : IRequest<CreateOrganiserResponse>
    {
        public CreateOrganiserRequest Dto { get; set; } 

        public class CreateOrganiserCommandHandler : IRequestHandler<CreateOrganiserCommand, CreateOrganiserResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public CreateOrganiserCommandHandler(
                IApplicationDbContext context,
                ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<CreateOrganiserResponse> Handle(CreateOrganiserCommand request, CancellationToken cancellationToken)
            {
                var user = _context.UserInfo.FirstOrDefault(x => x.IdentityId.Equals(_currentUserService));
                Address newAddress = new Address {
                    Street = request.Dto.Street,
                    Number = request.Dto.Number,
                    Appartment = request.Dto.Appartment,
                    PostalCode = request.Dto.PostalCode,
                    City = request.Dto.City
                };
                Organiser newOrganiser = new Organiser
                {
                    User = user,
                    Name = request.Dto.Name,
                    Description = request.Dto.Description,
                    Address = newAddress
                };

                _context.Organisers.Add(newOrganiser);
                await _context.SaveChangesAsync(cancellationToken);
                return new CreateOrganiserResponse
                {
                    Id = newOrganiser.Id,
                    Name = newOrganiser.Name,
                    Description = newOrganiser.Description,
                    Street = newAddress.Street,
                    Number = newAddress.Number,
                    Appartment = newAddress.Appartment,
                    PostalCode = newAddress.PostalCode,
                    City = newAddress.City
                };
            }
        }
    }
}
