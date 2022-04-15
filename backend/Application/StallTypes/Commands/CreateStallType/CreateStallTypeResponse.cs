using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StallTypes.Commands.CreateStallType
{
    public class CreateStallTypeResponse : Result
    {


        public CreateStallTypeResponse(Result result) : base(result.Succeeded, result.Errors)
        {
        }

        internal CreateStallTypeResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
