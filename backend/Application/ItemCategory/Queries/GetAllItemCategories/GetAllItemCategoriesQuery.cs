using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemCategory.Queries.GetAllItemCategories
{
    public class GetAllItemCategoriesQuery : IRequest<List<string>>
    {
        public class GetAllItemsCategoriesQueryHandler : IRequestHandler<GetAllItemCategoriesQuery, List<string>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllItemsCategoriesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<string>> Handle(GetAllItemCategoriesQuery request, CancellationToken cancellationToken)
            {
                return await _context.ItemCategories.Select(x => x.Name).ToListAsync();
            }
        }
    }
}
