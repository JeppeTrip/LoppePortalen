using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Stalls.Commands.DeleteStall
{
    public class DeleteStallCommand : IRequest<DeleteStallResponse>
    {
        public DeleteStallRequest Dto { get; set; }
        public class DeleteStallCommandHandler : IRequestHandler<DeleteStallCommand, DeleteStallResponse>
        {
            private readonly IApplicationDbContext _context;

            public DeleteStallCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<DeleteStallResponse> Handle(DeleteStallCommand request, CancellationToken cancellationToken)
            {
                var stall = _context.Stalls.FirstOrDefault(x => x.Id == request.Dto.StallId);
                if (stall == null)
                {
                    throw new NotFoundException($"No stall with id {stall.Id}.");
                }

                _context.Stalls.Remove(stall);
                await _context.SaveChangesAsync(cancellationToken);
                return new DeleteStallResponse(Common.Models.Result.Success());
            }
        }
    }
}
