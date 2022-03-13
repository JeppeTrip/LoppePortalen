using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Test.Commands
{
    public class TestCommand : IRequest<TestCommandResponse>
    {
        public class TestCommandHandler : IRequestHandler<TestCommand, TestCommandResponse>
        {
            public async Task<TestCommandResponse> Handle(TestCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => new TestCommandResponse() { TestResult="Test Command Has Run."} );
            }
        }
    }
}