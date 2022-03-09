using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class TestController : ApiBase
    {

        [HttpGet]
        public async Task<ActionResult<TestMessage>> GetTest()
        {
            return await Task.Run(() => new TestMessage { Message = $"Hello mother trucker" }); 
        }
    }

    public class TestMessage { 
        public string Message { get; set; }
    }
}
