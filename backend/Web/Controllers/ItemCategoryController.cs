using Application.ItemCategory.Queries.GetAllItemCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ItemCategoryController : ApiBase
    {
        [HttpGet("all")]
        public async Task<ActionResult<List<string>>> GetAllItemCategories()
        {
            return await Mediator.Send(new GetAllItemCategoriesQuery());
        }
    }
}
