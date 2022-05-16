using Application.Common.Models;
using System.Collections.Generic;

namespace Application.User.Queries.GetUser
{
    public class GetUserResponse : Result
    {
        internal GetUserResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public Common.Models.User User { get; set; }
    }
}
