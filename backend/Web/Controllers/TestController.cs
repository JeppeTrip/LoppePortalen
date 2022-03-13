using Application.Test.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Web.Controllers
{
    public class TestController : ApiBase
    {

        [HttpGet]
        public async Task<ActionResult<TestCommandResponse>> GetTest()
        {
            var result = await Mediator.Send(new TestCommand());
            return Ok(result);
        }
    }
}
