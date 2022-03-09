using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class TestController : ApiBase
    {

        [HttpGet]
        public async Task<ActionResult<string>> GetTest()
        {
            return await Task.Run(() => "Test");
        }
    }
}
