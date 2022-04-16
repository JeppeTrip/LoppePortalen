using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Stalls.Commands.RemoveStallFromMarket
{
    public class RemoveStallFromMarketCommand : IRequest<RemoveStallFromMarketResponse>
    {
        public class RemoveStallFromMarketCommandHandler : IRequestHandler<RemoveStallFromMarketCommand, RemoveStallFromMarketResponse>
        {
            public Task<RemoveStallFromMarketResponse> Handle(RemoveStallFromMarketCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
