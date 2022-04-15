using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Commands.CreateStallTypes
{
    public class CreateStallTypesResponse : Result
    {
        public CreateStallTypesResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public CreateStallTypesResponse(Result result) : base(result.Succeeded, result.Errors)
        {
        }

        public List<StallType> StallTypes { get; set; }
    }
}
