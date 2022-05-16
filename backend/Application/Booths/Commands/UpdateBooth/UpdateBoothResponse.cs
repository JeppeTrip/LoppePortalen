using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booths.Commands.UpdateBooth
{
    public class UpdateBoothResponse : Result
    {
        public UpdateBoothResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public UpdateBoothResponse(Result result) : base(result.Succeeded, result.Errors)
        {
        }
    }
}
