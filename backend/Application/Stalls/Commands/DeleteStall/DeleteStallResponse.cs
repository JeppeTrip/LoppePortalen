using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Stalls.Commands.DeleteStall
{
    public class DeleteStallResponse : Result
    {
        public DeleteStallResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public DeleteStallResponse(Result result) : base(result.Succeeded, result.Errors) { }
    }
}
