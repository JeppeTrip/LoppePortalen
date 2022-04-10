using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            public CreateOrganiserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<CreateOrganiserResponse> Handle(CreateOrganiserCommand request, CancellationToken cancellationToken)
            {
                var user = _context.UserInfo.FirstOrDefault(x => x.IdentityId.ToString().Equals(request.Dto.UserId));
                if(user == null)
                {
                    throw new ValidationException();
                }

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
