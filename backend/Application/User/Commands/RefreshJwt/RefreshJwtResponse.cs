using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.RefreshJwt
{
    public class RefreshJwtResponse : AuthResult
    {
        public RefreshJwtResponse(bool succeeded, IEnumerable<string> errors, string token, string refreshToken) : base(succeeded, errors, token, refreshToken) { }
    }
}
