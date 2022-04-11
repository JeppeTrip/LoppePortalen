using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Security.Test
{
    public class TestHandler : AuthorizationHandler<TestRequirement>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public TestHandler(
            IApplicationDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TestRequirement requirement)
        {
            if(context.User.IsInRole("Administator"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
