using Application.Common.Interfaces;
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

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ICurrentUserService CurrentUserService => _currentUserSerivce ??= HttpContext.RequestServices.GetService<ICurrentUserService>();

    }
}
