using Application.Common.Interfaces;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiBase : ControllerBase
    {
        private IMediator _mediator;
        private ICurrentUserService _currentUserSerivce;
        private ApplicationDbContext _context;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ICurrentUserService CurrentUserService => _currentUserSerivce ??= HttpContext.RequestServices.GetService<ICurrentUserService>();
        protected ApplicationDbContext Context => _context ??= HttpContext.RequestServices.GetService< ApplicationDbContext>();
    }
}
